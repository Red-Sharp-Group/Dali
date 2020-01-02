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
    class ColourBrightnessConverter : IValueConverter
    {
        /// <summary>
        /// Performs actual calculation of colour.
        /// </summary>
        /// <param name="colour">Base colour.</param>
        /// <param name="coef">Coeficient of brighness. Any number more or equat to 0. Values
        /// less than 1 cause darker colour, 1 the same one, larger then 1 - brighter.</param>
        /// <returns>Darker or brighter brush.</returns>
        private SolidColorBrush ModifyColour(Color colour, double coef)
        {
            byte R, G, B;

            R = Math.Min((byte)255, (byte)(colour.R * coef));
            G = Math.Min((byte)255, (byte)(colour.G * coef));
            B = Math.Min((byte)255, (byte)(colour.B * coef));

            return new SolidColorBrush(Color.FromArgb(colour.A, R, G, B));
        }

        /// <summary>
        /// Wrapper on <see cref="ModifyColour(Color, double)". Gets colour of brush and passes it to
        /// wrapped method./>
        /// </summary>
        /// <param name="brush">WPF brush. It's opacity is multiplied by 255 to produce alpha chanel value.</param>
        /// <param name="coef"><see cref="ModifyColour(Color, double)"/> coef description.</param>
        /// <returns><see cref="ModifyColour(Color, double)"/> return value.</returns>
        private SolidColorBrush ModifyBrush(SolidColorBrush brush, double coef)
        {
            Color colour = brush.Color;
            colour.A = (byte)(brush.Opacity * 255);

            return ModifyColour(colour, coef);
        }

        /// <summary>
        /// Makes given colour brighter or darker.
        /// </summary>
        /// <param name="value">Base colour. WPF <see cref="Color"/> and <see cref="SolidColorBrush"/> is supported.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter"><see cref="ModifyColour(Color, double)"/> coef description.</param>
        /// <param name="culture">The culture to use in the converter. Ignored here.</param>
        /// <returns><see cref="ModifyColour(Color, double)"/> return value.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            double coef;
            if (!double.TryParse(parameter.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out coef))
                throw new Exception("Not a number");

            if (value is Color colour)
            {
                SolidColorBrush toRet = ModifyColour(colour, coef);

                if (targetType.Equals(typeof(Color)))
                    return toRet.Color;
                else
                    return toRet;
            }
            else if (value is SolidColorBrush brush)
                return ModifyBrush(brush, coef);
            else
                throw new ArgumentException("Cannot work with such colour representation");
        }

        /// <summary>
        /// Not implemented now.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
