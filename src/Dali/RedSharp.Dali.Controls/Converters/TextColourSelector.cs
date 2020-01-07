using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Media;

namespace RedSharp.Dali.Controls.Converters
{
    /// <summary>
    /// Finds suitable text colour based on element's background.
    /// Algorithm based on article http://alienryderflex.com/hsp.html
    /// </summary>
    internal class TextColourSelector : IValueConverter
    {
        //Multipliers for every colour chanel
        private const double RedMult = .241;
        private const double GreenMult = .691;
        private const double BlueMult = .068;

        /// <summary>
        /// Calculates brigtness of background and determinates suitable foreground.
        /// </summary>
        /// <param name="colour">Background colour.</param>
        /// <returns>Black brush for bright background or white brush for dark background.</returns>
        internal SolidColorBrush FindSuitableForeground(Color colour)
        {
            double brightness = Math.Sqrt(colour.R * colour.R * RedMult + colour.G * colour.G * GreenMult + colour.B * colour.B * BlueMult);

            return brightness > 127 ? Brushes.Black : Brushes.White;
        }

        /// <summary>
        /// Wrapper on <see cref="FindSuitableForeground(Color)"/>. Gets colour of brush and passes it to wrapped method.
        /// </summary>
        /// <param name="brush">WPF brush. Backround.</param>
        /// <returns><see cref="FindSuitableForeground(Color)"/> return value.</returns>
        private SolidColorBrush FindSuitableForeground(SolidColorBrush brush)
        {
            return FindSuitableForeground(brush.Color);
        }

        /// <summary>
        /// Determinates foreground colour based on given background.
        /// </summary>
        /// <param name="value">Background colour. WPF <see cref="Color"/> and <see cref="SolidColorBrush"/> is supported.</param>
        /// <param name="targetType">The type of the binding target property.</param>
        /// <param name="parameter">Ignored here.</param>
        /// <param name="culture">The culture to use in the converter. Ignored here.</param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is Color colour)
                return FindSuitableForeground(colour);
            else if (value is SolidColorBrush brush)
                return FindSuitableForeground(brush);
            else
                throw new ArgumentException("Cannot work with such colour representation");
        }

        /// <summary>
        /// Not implemened.
        /// </summary>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
