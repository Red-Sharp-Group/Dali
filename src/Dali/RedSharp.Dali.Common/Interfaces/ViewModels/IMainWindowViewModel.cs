using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RedSharp.Dali.Common.Interfaces.ViewModels
{
    public interface IMainWindowViewModel : IDisposable
    {
        ICommand StartCommand { get; }

        ICommand LoadCommand { get; }

        ICommand RemoveCommand { get; }

        ICommand DragEnterCommand { get; }

        ICommand DragOverCommand { get; }

        ICommand DropCommand { get; }

        ISettingsProvider Settings { get; }

        ReadOnlyObservableCollection<IImageItem> Images { get; }
    }
}
