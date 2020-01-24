using RedSharp.Dali.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Interfaces.Services
{
    /// <summary>
    /// Abstraction to show windows without direct reference to any UI lib.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Show window without blocking thread.
        /// </summary>
        /// <param name="window">Window to show.</param>
        /// <param name="dataContext">DataContext for window. 
        /// If data context already set, in other way, it will not be overriden.</param>
        void ShowWindow(DaliWindowsEnum window, object dataContext = null);

        /// <summary>
        /// Show window as dialog with thread blocking.
        /// </summary>
        /// <param name="dialog">Dialog to show.</param>
        /// <param name="dataContext">DataContext for dialog.
        /// If data context already set, in other way, it will not be overriden.</param>
        void ShowDialog(DaliDialogsEnum dialog, object dataContext = null);

        /// <summary>
        /// Show given message with MessageBox window. Blocks thread.
        /// </summary>
        /// <param name="title">Title of the MessageBox.</param>
        /// <param name="message">Message to show.</param>
        /// <param name="icon">Icon for MessageBox.</param>
        /// <param name="buttons">Buttons set to show in MessageBox. 
        /// Each item represents one button. String - is a text for button and enum 
        /// is result of selecting that button.</param>
        /// <returns>Selected option.</returns>
        MessageBoxResult ShowMessageBox(string title, string message, MessageBoxIcon icon, IDictionary<MessageBoxResult, string> buttons);

        /// <summary>
        /// Shows open file dialog.
        /// </summary>
        /// <param name="initialFolder">Folder to show at launch.</param>
        /// <param name="filter">Filter string to show just some types of files.</param>
        /// <param name="options">Options will be applied to open file dialog.</param>
        /// <returns>Set of selected file (absolute paths). Empty if no file was selected.</returns>
        IEnumerable<string> ShowOpenFileDialog(string initialFolder, string filter, OpenFileDialogOptionsEnum options = OpenFileDialogOptionsEnum.CheckFileExists |
                                                                                                                        OpenFileDialogOptionsEnum.CheckPathExists |
                                                                                                                        OpenFileDialogOptionsEnum.Multiselect);

        /// <summary>
        /// Shows save file dialog.
        /// </summary>
        /// <param name="initialFolder">Folder to show at launch.</param>
        /// <returns>Selected path to save file and null if saving was canceled.</returns>
        string ShowSaveFileDialog(string initialFolder);
    }
}
