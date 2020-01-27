using Microsoft.Xaml.Behaviors;
using RedSharp.Dali.Common.Enums;
using RedSharp.Dali.Common.Events;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace RedSharp.Dali.Actions
{
    internal class ConvertDragAndDropArgsAction : TriggerAction<ListBox>
    {
        public static readonly DependencyProperty CommandProperty =
                   DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ConvertDragAndDropArgsAction));

        public ICommand Command
        {
            get => GetValue(CommandProperty) as ICommand;
            set => SetValue(CommandProperty, value);
        }

        protected override void Invoke(object parameter)
        {
            if (this.AssociatedObject != null && Command != null)
            {
                DragEventArgs eventArgs = parameter as DragEventArgs;

                DragAndDropEventArgs daliArgs = new DragAndDropEventArgs()
                {
                    AllowedEffects = ToDaliDragAndDropEffectsEnum(eventArgs.AllowedEffects),
                    Data = ToDragDataDictionary(eventArgs.Data),
                    Effects = ToDaliDragAndDropEffectsEnum(eventArgs.Effects),
                    Handled = eventArgs.Handled,
                    KeyStates = ToDaliDragAndDropKeyStatesEnum(eventArgs.KeyStates)
                };

                if (Command.CanExecute(daliArgs))
                {
                    Command.Execute(daliArgs);
                }

                eventArgs.Effects = ToWpfDragDropEffects(daliArgs.Effects);
                eventArgs.Handled = daliArgs.Handled;
            }
            else
            {
                //ToDo:
                //Add exception or trace message here.
            }
        }

        private DragAndDropEffectsEnum ToDaliDragAndDropEffectsEnum(DragDropEffects dragDropEffects)
        {
            int value = (int)dragDropEffects;

            if (value < 0)
                return DragAndDropEffectsEnum.Copy;

            if (Enum.TryParse(value.ToString(), out DragAndDropEffectsEnum result))
                return result;

            throw new ArgumentException("Cannot cast value");
        }

        private IDictionary<string, object> ToDragDataDictionary(IDataObject dataObject)
        {
            Dictionary<string, object> dataDictionary = new Dictionary<string, object>();

            foreach(string dataType in dataObject.GetFormats())
                dataDictionary.Add(dataType, dataObject.GetData(dataType));

            return dataDictionary;
        }

        private DragAndDropKeyStatesEnum ToDaliDragAndDropKeyStatesEnum(DragDropKeyStates keyStates)
        {
            if (Enum.TryParse(keyStates.ToString(), out DragAndDropKeyStatesEnum daliKeyStates))
                return daliKeyStates;

            throw new ArgumentException("Cannot cast value");
        }

        private DragDropEffects ToWpfDragDropEffects(DragAndDropEffectsEnum effectsEnum)
        {
            if (Enum.TryParse(effectsEnum.ToString(), out DragDropEffects wpfKeyStates))
                return wpfKeyStates;

            throw new ArgumentException("Cannot cast value");
        }
    }
}
