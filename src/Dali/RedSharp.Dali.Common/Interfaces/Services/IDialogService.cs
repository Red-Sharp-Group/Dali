using RedSharp.Dali.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Interfaces.Services
{
    public interface IDialogService
    {
        void ShowWindow(DaliWindows window, object dataContext = null);
        void ShowDialog(DaliDialogs dialogs, object dataContext = null);
        MessageBoxResult ShowMessageBox(string title, string message, MessageBoxIcon icon, IDictionary<MessageBoxResult, string> buttons);
        IEnumerable<string> ShowOpenFileDialog(string initialFolder, string filter);
        bool? ShowSaveFileDialog(string initialFolder);
    }
}
