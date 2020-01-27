using Microsoft.Xaml.Behaviors;
using RedSharp.Dali.Common.Enums;
using RedSharp.Dali.Common.Events;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public static readonly IReadOnlyDictionary<string, DropTypeEnum> DataTypesMapping
            = new ReadOnlyDictionary<string, DropTypeEnum>(new Dictionary<string, DropTypeEnum>()
            {
                [DataFormats.FileDrop] = DropTypeEnum.FilePath,
                [DataFormats.Bitmap] = DropTypeEnum.Bitmap,
                [DataFormats.Text] = DropTypeEnum.Text
            });

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

        private IDictionary<DropTypeEnum, object> ToDragDataDictionary(IDataObject dataObject)
        {
            Dictionary<DropTypeEnum, object> dataDictionary = new Dictionary<DropTypeEnum, object>();

            foreach (string dataType in DataTypesMapping.Keys)
            {
                if(dataObject.GetDataPresent(dataType))
                    dataDictionary.Add(DataTypesMapping[dataType], dataObject.GetData(dataType));
            }

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
