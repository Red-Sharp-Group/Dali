using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.ViewModel
{
    public class TransparentWindowViewModel : ReactiveObject
    {
        private bool _isTransparent;
        private ImageItem _item;

        public bool IsTransparent
        {
            get => _isTransparent;
            set => this.RaiseAndSetIfChanged(ref _isTransparent, value);
        }

        public ImageItem Item
        {
            get => _item;
        }

        public TransparentWindowViewModel(ImageItem item)
        {
            _item = item;
            _item.CreateImage();
        }

        public void Close()
        {
            
        }
    }
}
