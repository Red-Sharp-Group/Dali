using ReactiveUI;
using RedSharp.Dali.Common.Interfaces.ViewModels;
using System;

namespace RedSharp.Dali.ViewModel
{
    public class TransparentWindowViewModel : ReactiveObject, IDisposable
    {
        private bool _isTransparent;
        private double _opacity = 0.5;

        public bool IsTransparent
        {
            get => _isTransparent;
            set => this.RaiseAndSetIfChanged(ref _isTransparent, value);
        }

        public double Opacity
        {
            get => _opacity;
            set => this.RaiseAndSetIfChanged(ref _opacity, value);
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
