using RedSharp.Dali.Common.Enums;
using RedSharp.Dali.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Linq;
using Unity;
using RedSharp.Dali.Controls.Windows;
using System.Windows;

namespace RedSharp.Dali.View.Services
{
    /// <summary>
    /// Implementation of <see cref="IDialogService"/> for WPF.
    /// </summary>
    class DialogService : IDialogService
    {
        #region Static

        /// <summary>
        /// Generates filter string for Windows system dialogs.
        /// </summary>
        /// <param name="formats">Dictionary with file formats (key) and it's description.</param>
        /// <returns>Filter string for Windows system dialogs.</returns>
        private static string GenerateFilterString(IDictionary<string, string> formats)
        {
            return formats.Select(item => $"{item.Value}|{item.Key}").Aggregate((accum, val) => accum += $"|{val}").Trim('|');
        }

        #endregion

        #region Fields
        private readonly IUnityContainer _container;
        #endregion

        #region Construction
        public DialogService(IUnityContainer container)
        {
            _container = container;
        }
        #endregion

        #region Public methods

        /// <inheritdoc/>
        public void ShowDialog(DaliDialogsEnum dialogs, object dataContext = null)
        {
            ShowWindow(dialogs.ToString(), true, dataContext);
        }

        ///<inheritdoc/>
        public bool CloseWindow(object dataContext = null)
        {
            if (dataContext == null)
                return false;

            Window toClose = App.Current.Windows.OfType<Window>().FirstOrDefault(window => window.DataContext == dataContext);

            if (toClose == null)
                return false;

            toClose.Close();
            return true;
        }

        /// <inheritdoc/>
        public Common.Enums.MessageBoxResult ShowMessageBox(string title, string message, MessageBoxIcon icon, IDictionary<Common.Enums.MessageBoxResult, string> buttons)
        {
            System.Windows.MessageBoxResult rez = MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);

            if (rez == System.Windows.MessageBoxResult.OK)
                return Common.Enums.MessageBoxResult.Ok;

            throw new NotImplementedException();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="initialFolder"><inheritdoc/></param>
        /// <param name="filter">Filter string to show just some types of files. 
        /// Passed in format "Label|*.format1[;*.format2;*.formatN]"</param>
        /// <param name="options">Options will be applied to open file dialog.</param>
        /// <returns><inheritdoc/></returns>
        public IEnumerable<string> ShowOpenFileDialog(string initialFolder, 
                                                      IDictionary<string, string> formats, 
                                                      OpenFileDialogOptionsEnum options = OpenFileDialogOptionsEnum.CheckFileExists |
                                                                                          OpenFileDialogOptionsEnum.CheckPathExists |
                                                                                          OpenFileDialogOptionsEnum.Multiselect)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = initialFolder,
                Filter = GenerateFilterString(formats),
                CheckFileExists = options.HasFlag(OpenFileDialogOptionsEnum.CheckFileExists),
                CheckPathExists = options.HasFlag(OpenFileDialogOptionsEnum.CheckPathExists),
                Multiselect = options.HasFlag(OpenFileDialogOptionsEnum.Multiselect)
            };
            
            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileNames;
            else
                return Enumerable.Empty<string>();
        }

        /// <inheritdoc/>
        public string ShowSaveFileDialog(string initialFolder,
                                         IDictionary<string, string> formats)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                InitialDirectory = initialFolder,
                Filter = GenerateFilterString(formats),
                AddExtension = true,
                CheckPathExists = true
            };

            if (saveFileDialog.ShowDialog() == true)
                return saveFileDialog.FileName;
            else
                return null;
        }

        /// <inheritdoc/>
        public void ShowWindow(DaliWindowsEnum window, object dataContext = null)
        {
            ShowWindow(window.ToString(), false, dataContext);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Stores common code to show windows.
        /// </summary>
        /// <param name="key">Key to look in DI container.</param>
        /// <param name="isModal">Determins if window will be shown as modal one.</param>
        /// <param name="dataContext">Data context for window.</param>
        private void ShowWindow(string key, bool isModal, object dataContext = null)
        {
            Window window = _container.Resolve<Window>(key);

            if (window.DataContext == null)
                window.DataContext = dataContext;

            if (isModal)
                window.ShowDialog();
            else
                window.Show();
        }
        #endregion
    }
}
