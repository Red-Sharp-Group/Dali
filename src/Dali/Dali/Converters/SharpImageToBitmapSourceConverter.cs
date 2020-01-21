using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using RedSharp.Dali.Imaging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace RedSharp.Dali.Converters
{
    /// <summary>
    /// Converts <see cref="Image{TPixel}"/> into WPF compatible ImageSource. Only images with Rgba32
    /// pixel format might be converted.
    /// </summary>
    /// <remarks>
    /// Data specific converter. It should be kept in main project as it required for
    /// particular, object specific data transformation.
    /// </remarks>
    public class SharpImageToBitmapSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Image<Rgba32> image)
                return new ImageSharpImageSource<Rgba32>(image);

            throw new ArgumentException("Cannot convert image with such pixel type.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
