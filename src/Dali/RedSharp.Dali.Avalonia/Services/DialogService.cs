using RedSharp.Dali.Common.Enums;
using RedSharp.Dali.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using Unity;
using RedSharp.Dali.Avalonia.Controls.Windows;
using Avalonia.Controls;
using System.Threading.Tasks;
using Avalonia.Controls.ApplicationLifetimes;
using System.Threading;
using Avalonia.Threading;

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

        /// <inheritdoc/>
        public void ShowDialog(DaliDialogsEnum dialogs, object dataContext = null)
        {
            ShowWindow(dialogs.ToString(), true, dataContext);
        }

        ///<inheritdoc/>
        public bool CloseWindow(object dataContext = null)
        {
            /*if (dataContext == null)
                return false;

            Window toClose = App.Current.Windows.OfType<Window>().FirstOrDefault(window => window.DataContext == dataContext);

            if (toClose == null)*/
                return false;

            //toClose.Close();
            //return true;
        }

        /// <inheritdoc/>
        public MessageBoxResult ShowMessageBox(string title, string message, MessageBoxIcon icon, IDictionary<Common.Enums.MessageBoxResult, string> buttons)
        {
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
        public async Task<IEnumerable<string>> ShowOpenFileDialog(string initialFolder, 
                                                            string filter, 
                                                            OpenFileDialogOptionsEnum options = OpenFileDialogOptionsEnum.CheckFileExists |
                                                                                                OpenFileDialogOptionsEnum.CheckPathExists |
                                                                                                OpenFileDialogOptionsEnum.Multiselect)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                Directory = initialFolder,
                Filters = new List<FileDialogFilter>()
                {
                    new FileDialogFilter() { Name = "All files", Extensions = { "*" }},
                }
            };
            
            Window w = null;
            if(App.Current.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
                w = desktop.MainWindow;

            return await openFileDialog.ShowAsync(w);
        }

        /// <inheritdoc/>
        public async Task<string> ShowSaveFileDialog(string initialFolder)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                Directory = initialFolder,
                //CheckPathExists = true
            };

            return await saveFileDialog.ShowAsync(null);
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
                window.ShowDialog(null);
            else
                window.Show();
        }
        #endregion
    }
}
