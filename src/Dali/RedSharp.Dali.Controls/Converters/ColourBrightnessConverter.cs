using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace RedSharp.Dali.Controls.Converters
{
    class ColourBrightnessConverter : IValueConverter
    {
        private SolidColorBrush ModifyColour(Color colour, double coef)
        {
            byte R, G, B;

            R = Math.Min((byte)255, (byte)(colour.R * coef));
            G = Math.Min((byte)255, (byte)(colour.G * coef));
            B = Math.Min((byte)255, (byte)(colour.B * coef));

            return new SolidColorBrush(Color.FromArgb(colour.A, R, G, B));
        }

        private SolidColorBrush ModifyBrush(SolidColorBrush brush, double coef)
        {
            Color colour = brush.Color;
            colour.A = (byte)(brush.Opacity * 255);

            return ModifyColour(colour, coef);
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double coef;
            if (!double.TryParse(parameter.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out coef))
                throw new Exception("Not a number");

            if (value is Color colour)
                return ModifyColour(colour, coef);
            else if (value is SolidColorBrush brush)
                return ModifyBrush(brush, coef);
            else
                throw new ArgumentException("Cannot work with such colour representation");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
