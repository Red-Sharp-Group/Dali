using System;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.Common.Interfaces.ViewModels
{
    public interface IImageItem : IDisposable
    {
        object Image { get; }
        object Preview { get; }
        string Path { get; set; }

        bool IsSelected { get; set; }

        void CreateImage();
        void DisposeImage();
    }
}
