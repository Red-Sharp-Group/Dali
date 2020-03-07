using ReactiveUI;
using RedSharp.Dali.Common.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.ViewModel
{
    public class TransparentWindowViewModel : ReactiveObject, IDisposable
    {
        private bool _isTransparent;

        public bool IsTransparent
        {
            get => _isTransparent;
            set => this.RaiseAndSetIfChanged(ref _isTransparent, value);
        }

        public IImageItem Item { get; }

        public TransparentWindowViewModel(IImageItem item)
        {
            Item = item;
            Item.CreateImage();
        }

        public void Dispose()
        {
            Item.DisposeImage();
        }
    }
}
