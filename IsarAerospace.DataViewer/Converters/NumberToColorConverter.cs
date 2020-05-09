using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace IsarAerospace.DataViewer.Converters
{
    public class NumberToColorConverter : IValueConverter
    {
        public static double MaxGradientValue = 0;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var angle = (double.Parse(value.ToString()) / MaxGradientValue) * 90;
            LinearGradientBrush lgBrush = new LinearGradientBrush(Colors.White, Colors.Black,angle);
            return lgBrush;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
