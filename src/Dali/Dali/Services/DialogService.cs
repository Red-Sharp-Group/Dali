using RedSharp.Dali.Common.Enums;
using RedSharp.Dali.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Linq;
using Unity;
using RedSharp.Dali.Controls.Windows;
using System.Windows;

namespace RedSharp.Dali.Services
{
    class DialogService : IDialogService
    {
        private readonly IUnityContainer _container;

        public DialogService(IUnityContainer container)
        {
            _container = container;
        }

        public void ShowDialog(DaliDialogs dialogs, object dataContext = null)
        {
            ShowWindow(dialogs.ToString(), true, dataContext);
        }

        public Common.Enums.MessageBoxResult ShowMessageBox(string title, string message, MessageBoxIcon icon, IDictionary<Common.Enums.MessageBoxResult, string> buttons)
        {
            System.Windows.MessageBoxResult rez = MessageBox.Show(message, title, MessageBoxButton.OK, MessageBoxImage.Error);

            if (rez == System.Windows.MessageBoxResult.OK)
                return Common.Enums.MessageBoxResult.Ok;

            throw new NotImplementedException();
        }

        public IEnumerable<string> ShowOpenFileDialog(string initialFolder, string filter)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog()
            {
                InitialDirectory = initialFolder,
                Filter = filter,
                CheckFileExists = true,
                CheckPathExists = true
            };

            if (openFileDialog.ShowDialog() == true)
                return openFileDialog.FileNames;
            else
                return Enumerable.Empty<string>();
        }

        public bool? ShowSaveFileDialog(string initialFolder)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                InitialDirectory = initialFolder,
                CheckPathExists = true
            };

            return saveFileDialog.ShowDialog();
        }

        public void ShowWindow(DaliWindows window, object dataContext = null)
        {
            ShowWindow(window.ToString(), false, dataContext);
        }

        private void ShowWindow(string key, bool isModal, object dataContext = null)
        {
            DaliWindow window = _container.Resolve<DaliWindow>(key);

            if (window.DataContext == null)
                window.DataContext = dataContext;

            if (isModal)
                window.ShowDialog();
            else
                window.Show();
        }
    }
}
