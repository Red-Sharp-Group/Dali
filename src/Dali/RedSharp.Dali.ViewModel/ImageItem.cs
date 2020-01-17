using ReactiveUI;
using SixLabors.ImageSharp;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Text;

namespace RedSharp.Dali.ViewModel
{
    class ImageItem : ReactiveObject
    {
        public static bool CacheImages { get; set; }

        private Image _image;
        private string _path;
        private byte[] _cache;
        private Image _preview;
        public ImageItem(string path)
        { }

        public ImageItem(byte[] rawBites)
        { }

        /*public ImageItem(Memory<byte> memory)
        {
            MemoryHandle mem = memory.Pin();

            unsafe
            {
                Image.Load(new ReadOnlySpan<byte>(mem.Pointer, memory.Length));
            }
        }*/
    }
}
