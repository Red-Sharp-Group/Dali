using RedSharp.Dali.Common.Enums;
using RedSharp.Dali.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using Microsoft.Win32;
using System.Linq;

namespace RedSharp.Dali.Services
{
    class DialogService : IDialogService
    {
        public void ShowDialog(DaliDialogs dialogs, object dataContext = null)
        {
            throw new NotImplementedException();
        }

        public MessageBoxResult ShowMessageBox(string title, string message, MessageBoxIcon icon, IDictionary<MessageBoxResult, string> buttons)
        {
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
            throw new NotImplementedException();
        }
    }
}
