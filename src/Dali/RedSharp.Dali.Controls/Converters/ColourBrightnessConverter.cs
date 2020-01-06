using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace RedSharp.Dali.Controls.Converters
{
    /// <summary>
    /// Converter that calculates brighter and darker colours based on given one.
    /// </summary>
    internal class ColourBrightnessConverter : IValueConverter
    {
        /// <summary>
        /// Performs actual calculation of colour.
        /// </summary>
        /// <param name="colour">Base colour.</param>
        /// <param name="coef">Coeficient of brighness. Any number more or equat to 0. Values
        /// less than 1 cause darker colour, 1 the same one, larger then 1 - brighter.</param>
        /// <returns>Darker or brighter colour.</returns>
        internal Color ModifyColourBrightness(Color colour, double coef)
        {
            byte R, G, B;

            R = (byte)Math.Min(255, colour.R * coef);
            G = (byte)Math.Min(255, colour.G * coef);
            B = (byte)Math.Min(255, colour.B * coef);

            return Color.FromArgb(colour.A, R, G, B);
        }

        /// <summary>
        /// Wrapper on <see cref="ModifyColourBrightness(Color, double)". Gets colour of brush and passes it to
        /// wrapped method./>
        /// </summary>
        /// <param name="brush">WPF brush. It's opacity is multiplied by 255 to produce alpha chanel value.</param>
        /// <param name="coef"><see cref="ModifyColourBrightness(Color, double)"/> coef description.</param>
        /// <returns>New darker or brighter brush.</returns>
        private SolidColorBrush ModifyBrushBrightness(SolidColorBrush brush, double coef)
        {
            Color colour = brush.Color;
            colour.A = (byte)(brush.Opacity * 255);

            return new SolidColorBrush(ModifyColourBrightness(colour, coef));
        }

        /// <summary>
        /// Makes given colour brighter or darker.
        /// </summary>
        /// <param name="value">Base colour. WPF <see cref="Color"/> and <see cref="SolidColorBrush"/> is supported.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter"><see cref="ModifyColourBrightness(Color, double)"/> coef description.</param>
        /// <param name="culture">The culture to use in the converter. Ignored here.</param>
        /// <returns><see cref="ModifyColourBrightness(Color, double)"/> return value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double coef;
            if (!double.TryParse(parameter.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out coef))
                throw new Exception("Not a number");

            if (value is Color colour)
            {
                Color toRet = ModifyColourBrightness(colour, coef);

                if (targetType.Equals(typeof(Color)))
                    return toRet;
                else
                    return new SolidColorBrush(toRet);
            }
            else if (value is SolidColorBrush brush)
                return ModifyBrushBrightness(brush, coef);
            else
                throw new ArgumentException("Cannot work with such colour representation");
        }

        /// <summary>
        /// Not implemented now.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
