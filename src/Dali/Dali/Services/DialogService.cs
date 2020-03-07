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
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="dialogs"><inheritdoc/></param>
        /// <param name="dataContext"><inheritdoc/></param>
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

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="title"><inheritdoc/></param>
        /// <param name="message"><inheritdoc/></param>
        /// <param name="icon"><inheritdoc/></param>
        /// <param name="buttons"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
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
                                                      string filter, 
                                                      OpenFileDialogOptionsEnum options = OpenFileDialogOptionsEnum.CheckFileExists |
                                                                                          OpenFileDialogOptionsEnum.CheckPathExists |
                                                                                          OpenFileDialogOptionsEnum.Multiselect)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = initialFolder,
                Filter = filter,
                CheckFileExists = options.HasFlag(OpenFileDialogOptionsEnum.CheckFileExists),
                CheckPathExists = options.HasFlag(OpenFileDialogOptionsEnum.CheckPathExists),
                Multiselect = options.HasFlag(OpenFileDialogOptionsEnum.Multiselect)
            };
            
            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileNames;
            else
                return Enumerable.Empty<string>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="initialFolder"><inheritdoc/></param>
        /// <returns><inheritdoc/></returns>
        public string ShowSaveFileDialog(string initialFolder)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                InitialDirectory = initialFolder,
                CheckPathExists = true
            };

            if (saveFileDialog.ShowDialog() == true)
                return saveFileDialog.FileName;
            else
                return null;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="window"><inheritdoc/></param>
        /// <param name="dataContext"><inheritdoc/></param>
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
