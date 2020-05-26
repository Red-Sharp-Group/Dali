using ReactiveUI;
using RedSharp.Dali.Common.Interfaces.ViewModels;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using System;
using System.IO;

namespace RedSharp.Dali.ViewModel
{
    /// <summary>
    /// View Model for storing image.
    /// Wrapps <see cref="Image{TPixel}"/>.
    /// </summary>
    public class ImageItem : ReactiveObject, IImageItem
    {
        #region Static
        /// <summary>
        /// Parameter that identifies files bufferization. I think there will 
        /// be opportunity to set from app settings in the future.
        /// </summary>
        public static bool CacheImages { get; set; }
        #endregion

        #region Fields
        private bool _isSelectd;
        private string _path;
        private byte[] _cache;
        private Image _preview;
        #endregion

        #region Construction

        /// <summary>
        /// Constructs <see cref="ImageItem"/> object. Caches imput files if <see cref="ImageItem.CacheImages"/> 
        /// property is set.
        /// </summary>
        /// <param name="path">Path to file with image. File should exist.</param>
        /// <remarks>
        /// Throws <see cref="FileNotFoundException"/> if file is not found.
        /// </remarks>
        public ImageItem(string path)
        {
            //We can create IOService in future to ease unit testing.
            if (!File.Exists(path))
                throw new FileNotFoundException($"{nameof(path)} is not exists.");

            _path = path;
            if (CacheImages)
            {
                _cache = ReadBytesFromFile(_path);
            }
        }

        /// <summary>
        /// Constructs <see cref="ImageItem"/> object.
        /// </summary>
        /// <param name="rawBytes">Array with image file. Not copied.</param>
        public ImageItem(byte[] rawBytes)
        {
            if (rawBytes == null || rawBytes.Length == 0)
                throw new ArgumentNullException(nameof(rawBytes), "Buffer is null or empty.");

            _cache = rawBytes;
        }

        // For future use. Maybe this approach is faster...
        /*public ImageItem(Memory<byte> memory)
        {
            MemoryHandle mem = memory.Pin();

            unsafe
            {
                Image.Load(new ReadOnlySpan<byte>(mem.Pointer, memory.Length));
            }
        }*/
        #endregion

        #region Public Properties

        /// <summary>
        /// Stores fully loaded image. Should be set only for image on work area.
        /// </summary>
        public Image Image { get; private set; }

        /// <summary>
        /// Path to image file. Might be path to non existing file (for further saving).
        /// </summary>
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

        /// <summary>
        /// Reduced image to show in list box.
        /// </summary>
        public Image Preview
        {
            get
            {
                if (_preview == null)
                {
                    using (Image fullImage = Image.Load(Cache))
                    {
                        _preview = fullImage.Clone(image => image.Resize(new ResizeOptions() { Mode = ResizeMode.Max, Size = new Size(256, 256) }));
                    }
                }

                return _preview;
            }
        }

        /// <summary>
        /// Gets or sets is item is currently selected in the list.
        /// </summary>
        public bool IsSelected
        {
            get
            {
                return _isSelectd;
            }
            set
            {
                this.RaiseAndSetIfChanged(ref _isSelectd, value);
            }
        }
        #endregion

        #region Private Properties
        /// <summary>
        /// Incupsulates logic for buffer reading. Returns cached images or tries to load it from saved path.
        /// </summary>
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
                    if (string.IsNullOrEmpty(_path) || !File.Exists(_path))
                        throw new FileNotFoundException("File path is invalid. Cannot load image.");

                    return ReadBytesFromFile(_path);
                }
            }
        }


        #endregion

        #region Public Methods
        /// <summary>
        /// Explicitly creates loads full size image.
        /// </summary>
        /// <remarks>I think it should be called to confirm that full image should be stored
        /// in memory.</remarks>
        public void CreateImage()
        {
            Image = Image.Load(Cache);
        }
        #endregion

        #region Private Methods
        /// <summary>
        /// Reads bytes from files.
        /// </summary>
        /// <param name="path">Path to file. Path is not validated.</param>
        /// <returns>Content of file.</returns>
        private byte[] ReadBytesFromFile(string path)
        {
            using (FileStream file = File.OpenRead(path))
            {
                byte[] buffer = new byte[file.Length];
                file.Read(buffer, 0, (int)file.Length);
                return buffer;
            }
        }

        #endregion

        #region IImageItem

        object IImageItem.Image { get => Image; }
        object IImageItem.Preview { get => Preview; }

        #endregion

        #region Disposable
        public void DisposeImage()
        {
            if (Image != null)
                Image.Dispose();
        }

        /// <summary>
        /// Disposes loaded image and it's preview.
        /// </summary>
        public void Dispose()
        {
            DisposeImage();

            if (_preview != null)
                _preview.Dispose();
        }
        #endregion
    }
}
