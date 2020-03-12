using ReactiveUI;
using RedSharp.Dali.Common.Interfaces.ViewModels;
using System;

namespace RedSharp.Dali.ViewModel
{
    /// <summary>
    /// View model for transparent window.
    /// </summary>
    public class TransparentWindowViewModel : ReactiveObject, IDisposable
    {
        #region Fields

        private bool _isTransparent;
        private double _opacity = 0.5;

        #endregion

        /// <summary>
        /// Gets or sets value that indicates is window handles input (mouse, keyboard, gestures) events.
        /// </summary>
        public bool IsTransparent
        {
            get => _isTransparent;
            set => this.RaiseAndSetIfChanged(ref _isTransparent, value);
        }

        /// <summary>
        /// Gets or sets valus of alpha chanel for window.
        /// </summary>
        public double Opacity
        {
            get
            {
                return _opacity;
            }
            set
            {
                double valueToSet = value;

                if (value < 0)
                    valueToSet = 0;
                else if (value > 1)
                    valueToSet = 1;

                this.RaiseAndSetIfChanged(ref _opacity, valueToSet);
            }
        }

        /// <summary>
        /// Image item to show.
        /// </summary>
        /// <remarks>
        /// Might be several items in future.
        /// </remarks>
        public IImageItem Item { get; }


        /// <summary>
        /// Creates new instance of <see cref="TransparentWindowViewModel"/>.
        /// </summary>
        /// <param name="item">Item to present.</param>
        public TransparentWindowViewModel(IImageItem item)
        {
            Item = item;
            Item.CreateImage();
        }

        /// <summary>
        /// Disposes full size image. But <see cref="Item"/> is not fully disposed.
        /// </summary>
        public void Dispose()
        {
            Item.DisposeImage();
        }
    }
}
