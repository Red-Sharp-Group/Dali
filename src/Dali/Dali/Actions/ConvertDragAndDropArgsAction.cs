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

namespace RedSharp.Dali.View.Actions
{
    /// <summary>
    /// Custom trigger action for executing command from event trigger (for drag and drop events)
    /// with back conversation of event args. 
    /// </summary>
    /// <remarks>
    /// Based on <see cref="Microsoft.Xaml.Behaviors.TriggerAction{T}"/> because <see cref="System.Windows.TriggerAction"/>
    /// has only internal constructor.
    /// </remarks>
    internal class ConvertDragAndDropArgsAction : TriggerAction<ListBox>
    {
        /// <summary>
        /// Dependency property to bind command from View Model.
        /// </summary>
        public static readonly DependencyProperty CommandProperty =
                   DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(ConvertDragAndDropArgsAction));

        /// <summary>
        /// Mapping from WPF string <see cref="DataFormats"/> data identifiers to cross platform <see cref="DropTypeEnum"/>
        /// data identifiyers.
        /// </summary>
        public static readonly IReadOnlyDictionary<string, DropTypeEnum> DataTypesMapping
            = new ReadOnlyDictionary<string, DropTypeEnum>(new Dictionary<string, DropTypeEnum>()
            {
                [DataFormats.FileDrop] = DropTypeEnum.FilePath,
                [DataFormats.Bitmap] = DropTypeEnum.Bitmap,
                [DataFormats.Text] = DropTypeEnum.Text
            });

        /// <summary>
        /// Gets or sets command to execute. Dependency property.
        /// </summary>
        public ICommand Command
        {
            get => GetValue(CommandProperty) as ICommand;
            set => SetValue(CommandProperty, value);
        }

        /// <summary>
        /// Method invoked on trigger firing.
        /// </summary>
        /// <param name="parameter">WPF drag and drop event args.</param>
        protected override void Invoke(object parameter)
        {
            if (AssociatedObject != null && Command != null)
            {
                DragEventArgs eventArgs = parameter as DragEventArgs;

                if (eventArgs == null)
                    throw new ArgumentException($"{nameof(parameter)} is not a {nameof(DragEventArgs)}");

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
                throw new InvalidOperationException("Cannot invoke this action because not all required properties are set.");
            }
        }

        //I had thought about extension methods but didn't want to add new static class for it.
        #region Converter methods

        /// <summary>
        /// Converts WPF <see cref="DragDropEffects"/> to Dali's crossplatform representation.
        /// </summary>
        /// <param name="dragDropEffects">Value to convert.</param>
        /// <returns>Correspoding value of crossplatform enum.</returns>
        /// <remarks>
        /// <see cref="DragDropEffects.All"/> and <see cref="DragDropEffects.Scroll"/> threatened as <see cref="DragDropEffects.Copy"/>.
        /// </remarks>
        private DragAndDropEffectsEnum ToDaliDragAndDropEffectsEnum(DragDropEffects dragDropEffects)
        {
            int value = (int)dragDropEffects;

            if (value < 0)
                return DragAndDropEffectsEnum.Copy;

            if (Enum.TryParse(value.ToString(), out DragAndDropEffectsEnum result))
                return result;

            throw new ArgumentException("Cannot cast value");
        }

        /// <summary>
        /// Converts WPF <see cref="IDataObject"/> to Dali's crossplatform representation.
        /// </summary>
        /// <param name="dataObject">Object to convert.</param>
        /// <returns>
        /// Dictionary filled with data from passed IDataObject if it containes one. Empty one if there are
        /// nothing suitable.
        /// </returns>
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

        /// <summary>
        /// Converts <see cref="DragDropKeyStates"/> to Dali's crossplatform representation.
        /// </summary>
        /// <param name="keyStates">Value to convert.</param>
        /// <returns>Corresponding value of crossplatform enum.</returns>
        private DragAndDropKeyStatesEnum ToDaliDragAndDropKeyStatesEnum(DragDropKeyStates keyStates)
        {
            if (Enum.TryParse(keyStates.ToString(), out DragAndDropKeyStatesEnum daliKeyStates))
                return daliKeyStates;

            throw new ArgumentException("Cannot cast value");
        }

        /// <summary>
        /// Converts <see cref="DragAndDropEffectsEnum"/> back to WPF representation.
        /// </summary>
        /// <param name="effectsEnum">Value to convert</param>
        /// <returns>Corresponding value of WPF enum.</returns>
        private DragDropEffects ToWpfDragDropEffects(DragAndDropEffectsEnum effectsEnum)
        {
            if (Enum.TryParse(effectsEnum.ToString(), out DragDropEffects wpfKeyStates))
                return wpfKeyStates;

            throw new ArgumentException("Cannot cast value");
        }
        #endregion
    }
}
