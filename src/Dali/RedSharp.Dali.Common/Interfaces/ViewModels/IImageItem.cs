using System;

namespace RedSharp.Dali.Common.Interfaces.ViewModels
{
    /// <summary>
    /// Exposes information about opened image.
    /// </summary>
    public interface IImageItem : IDisposable
    {
        /// <summary>
        /// Actual Image implementation.
        /// </summary>
        object Image { get; }

        /// <summary>
        /// Implementation of image preview.
        /// </summary>
        object Preview { get; }

        /// <summary>
        /// Path for file with image.
        /// </summary>
        /// <remarks>
        /// TODO: Think about something like Path class or Uri.
        /// </remarks>
        string Path { get; set; }

        /// <summary>
        /// Gets or sets value that indicates is current object selected for processing. 
        /// </summary>
        bool IsSelected { get; set; }

        /// <summary>
        /// Loads image from <see cref="Path"/> in full size.
        /// </summary>
        void CreateImage();

        /// <summary>
        /// Disposes full size image.
        /// </summary>
        void DisposeImage();
    }
}
