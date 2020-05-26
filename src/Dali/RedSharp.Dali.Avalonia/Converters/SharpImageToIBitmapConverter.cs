using System;
using System.Globalization;
using Avalonia.Data.Converters;
using Avalonia.Media.Imaging;
using Avalonia.Utilities;
using RedSharp.Dali.Avalonia.Imaging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace RedSharp.Dali.View.Converters
{
    public class SharpImageToIBitmapConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Image<Rgba32> image)
                return new ImageSharpImageSource<Rgba32>(image);;           

            throw new ArgumentException("Cannot convert image with such pixel type.");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}