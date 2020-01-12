using ReactiveUI;
using RedSharp.Dali.Common.Interfaces.Services;
using SixLabors.ImageSharp;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;

namespace RedSharp.Dali.ViewModel
{
    /// <summary>
    /// View model for main application window.
    /// </summary>
    public class MainWindowViewModel : ReactiveObject
    {
        /// <summary>
        /// Filter string for OpenFileDialog should be mvoed to configs.
        /// </summary>
        private const string FilterString = "Images|*.bmp;*.jpg;*.jpeg;*.png;*.tiff";

        #region Fields
        private readonly IDialogService _dialogService;

        private ReactiveCommand<Unit, Unit> _startCommand;
        private ReactiveCommand<Unit, Unit> _loadCommand;
        private ReactiveCommand<Unit, Unit> _removeCommand;

        #endregion

        #region Construction
        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        #endregion

        #region Commands

        /// <summary>
        /// Command that invoked to launch work window with selected images.
        /// </summary>
        public ReactiveCommand<Unit, Unit> StartCommand
        {
            get
            {
                return _startCommand ?? (_startCommand = ReactiveCommand.Create(() =>
               {

               }));
            }
        }

        /// <summary>
        /// Command that invoked to open files with OpenFileDialog.
        /// </summary>
        public ReactiveCommand<Unit, Unit> LoadCommand
        {
            get
            {
                return _loadCommand ?? (_loadCommand = ReactiveCommand.Create(() =>
                {
                    IEnumerable<string> files = _dialogService.ShowOpenFileDialog(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FilterString);
                    foreach (string file in files)
                    {
                        Images.Add(Image.Load(file));
                    }
                }));
            }
        }

        /// <summary>
        /// Command that invoked to remove opened images.
        /// </summary>
        public ReactiveCommand<Unit, Unit> RemoveCommand
        {
            get
            {
                return _removeCommand ?? (_removeCommand = ReactiveCommand.Create(() =>
                {

                }));
            }
        }
        #endregion

        #region Public Properties

        public ObservableCollection<Image> Images { get; } = new ObservableCollection<Image>();

        #endregion
    }
}
