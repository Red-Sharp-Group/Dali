using ReactiveUI;
using RedSharp.Dali.Common.Interfaces.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.ViewModel
{
    public class TransparentWindowViewModel : ReactiveObject
    {
        private bool _isTransparent;
        private IImageItem _item;

        public bool IsTransparent
        {
            get => _isTransparent;
            set => this.RaiseAndSetIfChanged(ref _isTransparent, value);
        }

        public IImageItem Item
        {
            get => _item;
        }

        public TransparentWindowViewModel(IImageItem item)
        {
            _item = item;
            _item.CreateImage();
        }

        public void Close()
        {
            
        }
    }
}
