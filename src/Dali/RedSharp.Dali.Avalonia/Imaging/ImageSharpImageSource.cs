using Avalonia;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using Avalonia.Utilities;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace RedSharp.Dali.Avalonia.Imaging
{
    class ImageSharpImageSource<TPixel> : IBitmap
        where TPixel : unmanaged, IPixel<TPixel>
    {
        private readonly Image<TPixel> _source;
        private Vector _dpi;
        public ImageSharpImageSource(Image<TPixel> source)
        {
            _source = source;
            MemoryStream stream = new MemoryStream();

            IPlatformRenderInterface factory = AvaloniaLocator.Current.GetService<IPlatformRenderInterface>();
            
            /*for(int i = 0; i<_source.Height; i++)
            {
                for(int j = 0; j<_source.Width; j++)
                {
                    Rgba32 dest = default;
                    _source[j, i].ToRgba32(ref dest);
                    
                    stream.Write(new byte[] {dest.R, dest.G, dest.B, dest.A}, 0, 4);
                }
            }*/
            _source.SaveAsPng(stream);

            stream.Seek(0, SeekOrigin.Begin);
            PlatformImpl = RefCountable.Create(factory.LoadBitmap(stream));
        }

        public Vector Dpi => PlatformImpl.Item.Dpi;

        public PixelSize PixelSize => PlatformImpl.Item.PixelSize;

        public int Version => 1;

        public IRef<IBitmapImpl> PlatformImpl
        {
            get;
        }

        public global::Avalonia.Size Size => PlatformImpl.Item.PixelSize.ToSizeWithDpi(Dpi);

        public void Dispose()
        {
            _source.Dispose();
        }

        public void Save(string fileName)
        {
            
            if (Path.GetExtension(fileName) != ".png")
            {
                // Yeah, we need to support other formats.
                throw new NotSupportedException("Use PNG, stoopid.");
            }

            using (FileStream s = new FileStream(fileName, FileMode.Create))
            {
                Save(s);
            }
        }

        public void Save(Stream stream)
        {
            _source.SaveAsPng(stream);
        }
    }
}