using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace RedSharp.Dali.Controls.Converters
{
    class TextColorSelector : IValueConverter
    {
        private SolidColorBrush ModifyColour(Color c)
        {
            double brightness = Math.Sqrt(c.R * c.R * .241 + c.G * c.G * .691 + c.B * c.B * .068);

            return brightness > 127 ? Brushes.Black : Brushes.White;
        }

        private SolidColorBrush ModifyBrush(SolidColorBrush brush)
        {
            Color colour = brush.Color;
            colour.A = (byte)(brush.Opacity * 255);

            return ModifyColour(colour);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color colour)
                return ModifyColour(colour);
            else if (value is SolidColorBrush brush)
                return ModifyBrush(brush);
            else
                throw new ArgumentException("Cannot work with such colour representation");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
