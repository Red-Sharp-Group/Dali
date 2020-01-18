using ReactiveUI;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;

namespace RedSharp.Dali.ViewModel
{
    public class ImageItem : ReactiveObject, IDisposable
    {
        public static bool CacheImages { get; set; }

        private Image _image;
        private string _path;
        private byte[] _cache;
        private IReadOnlyCollection<byte> _readOnlyCache;
        private Image _preview;
        public ImageItem(string path)
        {
            if (!File.Exists(path))
                throw new ArgumentException($"{nameof(path)} is not exists.");

            _path = path;
            if (CacheImages)
            {
                using (FileStream file = File.OpenRead(path))
                {
                    _cache = new byte[file.Length];
                    file.Read(_cache, 0, (int)file.Length);
                }

                _readOnlyCache = new ReadOnlyCollection<byte>(_cache);
            }
        }

        public ImageItem(byte[] rawBytes)
        {
            if (rawBytes == null || rawBytes.Length == 0)
                throw new ArgumentNullException(nameof(rawBytes), "Buffer is null or empty.");

            _cache = rawBytes;
            _readOnlyCache = new ReadOnlyCollection<byte>(rawBytes);
        }

        /*public ImageItem(Memory<byte> memory)
        {
            MemoryHandle mem = memory.Pin();

            unsafe
            {
                Image.Load(new ReadOnlySpan<byte>(mem.Pointer, memory.Length));
            }
        }*/

        public Image Image
        {
            get
            {
                return _image;
            }
        }

        public string Path
        {
            get
            {
                return _path;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _path, value);
            }
        }

        public IReadOnlyCollection<byte> Buffer
        {
            get
            {
                return _readOnlyCache;
            }
        }

        public Image Preview
        {
            get
            {
                if (_preview == null)
                {
                    using (Image fullImage = Image.Load(Cache))
                    {
                        _preview = fullImage.Clone(image => image.Resize(Math.Min(fullImage.Width, 128), Math.Min(fullImage.Height, 128)));
                    }
                }

                return _preview;
            }
        }

        private byte[] Cache
        {
            get
            {
                if (CacheImages || _cache != null)
                {
                    return _cache;
                }
                else
                {
                    using (FileStream file = File.OpenRead(_path))
                    {
                        byte[] buffer = new byte[file.Length];
                        file.Read(buffer, 0, (int)file.Length);
                        return buffer;
                    }
                }
            }
        }

        public void CreateImage()
        {
            _image = Image.Load(Cache);
        }

        public void Dispose()
        {
            if (_image != null && !_image.IsDisposed)
                _image.Dispose();

            if (_preview != null && !_preview.IsDisposed)
                _preview.Dispose();
        }
    }
}
