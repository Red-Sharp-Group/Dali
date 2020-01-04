using ReactiveUI;
using System;
using System.Reactive;

namespace RedSharp.Dali.ViewModel
{
    public class MainWindowViewModel : ReactiveObject
    {
        private ReactiveCommand<Unit, Unit> _startCommand;
        private ReactiveCommand<Unit, Unit> _loadCommand;
        private ReactiveCommand<Unit, Unit> _removeCommand;

        public MainWindowViewModel()
        {
            
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
