using ReactiveUI;
using RedSharp.Dali.Common.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Reactive;

namespace RedSharp.Dali.ViewModel
{
    public class MainWindowViewModel : ReactiveObject
    {
        private const string FilterString = "Images|*.bmp;*.jpg;*.jpeg;*.png;*.tiff";

        private IDialogService _dialogService;

        private ReactiveCommand<Unit, Unit> _startCommand;
        private ReactiveCommand<Unit, Unit> _loadCommand;
        private ReactiveCommand<Unit, Unit> _removeCommand;

        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public ReactiveCommand<Unit, Unit> StartCommand
        {
            get
            {
                return _startCommand ?? ( _startCommand = ReactiveCommand.Create(() => 
                {

                }));
            }
        }

        public ReactiveCommand<Unit, Unit> LoadCommand
        {
            get
            {
                return _loadCommand ?? (_loadCommand = ReactiveCommand.Create(() =>
                {
                    IEnumerable<string> files = _dialogService.ShowOpenFileDialog(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FilterString);
                }));
            }
        }

        public ReactiveCommand<Unit, Unit> RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = ReactiveCommand.Create(() =>
                {

                }));
            }
        }
    }
}
