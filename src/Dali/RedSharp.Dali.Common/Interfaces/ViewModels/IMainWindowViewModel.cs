using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace RedSharp.Dali.Common.Interfaces.ViewModels
{
    public interface IMainWindowViewModel : IDisposable
    {
        /// <summary>
        /// Command that invoked to launch work window with selected images.
        /// </summary>
        ICommand StartCommand { get; }

        /// <summary>
        /// Command that invoked to open files with OpenFileDialog.
        /// </summary>
        ICommand LoadCommand { get; }

        /// <summary>
        /// Command that invoked to save files with SaveFileDialog.
        /// </summary>
        ICommand SaveCommand { get; }

        /// <summary>
        /// Command that invoked to remove opened images.
        /// </summary>
        ICommand RemoveCommand { get; }

        /// <summary>
        /// Command that invoked when something is draged onto image list.
        /// </summary>
        ICommand DragEnterCommand { get; }

        /// <summary>
        /// Command that invoked when something is draged over the image list.
        /// </summary>
        ICommand DragOverCommand { get; }

        /// <summary>
        /// Command that invoked when something is droped onto image list.
        /// </summary>
        ICommand DropCommand { get; }

        /// <summary>
        /// Gets application settings.
        /// </summary>
        ISettingsProvider Settings { get; }

        /// <summary>
        /// Collection with all opened images.
        /// </summary>
        ReadOnlyObservableCollection<IImageItem> Images { get; }
    }
}
