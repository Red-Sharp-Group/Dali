using ReactiveUI;
using RedSharp.Dali.Common.Interfaces.Services;
using SixLabors.ImageSharp;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reactive;
using DynamicData.Binding;
using RedSharp.Dali.Common.Enums;
using System.Reactive.Linq;
using DynamicData;
using RedSharp.Dali.Common.Events;

namespace RedSharp.Dali.ViewModel
{
    /// <summary>
    /// View model for main application window.
    /// </summary>
    public class MainWindowViewModel : ReactiveObject, IDisposable
    {
        /// <summary>
        /// Filter string for OpenFileDialog should be mvoed to configs.
        /// </summary>
        private const string FilterString = "Images|*.bmp;*.jpg;*.jpeg;*.png;*.tiff";

        #region Fields
        private readonly IDialogService _dialogService;

        //Items to save open images.
        //Actual storage. Please work with it.
        private SourceList<ImageItem> _images = new SourceList<ImageItem>();

        //Buffer for public access. Here might be transformed or filtered items if 
        //such operation will be applied.
        private ReadOnlyObservableCollection<ImageItem> _readOnlyBuff;

        //Subscription holder.
        private readonly IDisposable _imagesSubscription;

        private ReactiveCommand<Unit, Unit> _startCommand;
        private ReactiveCommand<Unit, Unit> _loadCommand;
        private ReactiveCommand<Unit, Unit> _removeCommand;

        private ReactiveCommand<DragAndDropEventArgs, Unit> _dragEnterCommand;
        private ReactiveCommand<DragAndDropEventArgs, Unit> _dragOverCommand;
        private ReactiveCommand<DragAndDropEventArgs, Unit> _dropCommand;

        #endregion

        #region Construction
        public MainWindowViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            _imagesSubscription = _images.Connect().Bind(out _readOnlyBuff).Subscribe();
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
                   _dialogService.ShowWindow(DaliWindowsEnum.WorkAreaWindow);
               }, _images.Connect().WhenPropertyChanged(item => item.IsSelected).Select(res => res.Value)));
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
                        _images.Add(new ImageItem(file));
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
                    IEnumerable<ImageItem> selected = Images.Where(image => image.IsSelected).ToArray();
                    foreach (ImageItem item in selected)
                    {
                        item.IsSelected = false;
                        _images.Remove(item);
                    }
                }, _images.Connect().WhenPropertyChanged(item=>item.IsSelected).Select(res => res.Value)));
            }
        }

        public ReactiveCommand<DragAndDropEventArgs, Unit> DragEnterCommand
        {
            get
            {
                return _dragEnterCommand ?? (_dragEnterCommand = ReactiveCommand.Create<DragAndDropEventArgs>(args =>
                {
                    args.Effects = DragAndDropEffectsEnum.Move;
                }));
            }
        }

        public ReactiveCommand<DragAndDropEventArgs, Unit> DragOverCommand
        {
            get
            {
                return _dragOverCommand ?? (_dragOverCommand = ReactiveCommand.Create<DragAndDropEventArgs>(args =>
                {

                }));
            }
        }

        public ReactiveCommand<DragAndDropEventArgs, Unit> DropCommand
        {
            get
            {
                return _dropCommand ?? (_dropCommand = ReactiveCommand.Create<DragAndDropEventArgs>(args =>
                {

                }));
            }
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// Collection with all opened images.
        /// </summary>
        public ReadOnlyObservableCollection<ImageItem> Images { get => _readOnlyBuff; }

        #endregion

        #region Disposable
        /// <summary>
        /// Disposes internal subscriptions, reactive commands and opened images.
        /// </summary>
        public void Dispose()
        {
            _imagesSubscription.Dispose();

            StartCommand.Dispose();
            LoadCommand.Dispose();
            RemoveCommand.Dispose();

            foreach (ImageItem item in _images.Items)
                item.Dispose();

            _images.Dispose();
        }

        #endregion
    }
}
