
using System;
using System.Globalization;
using System.Windows.Data;

namespace MotionRobot.Common.Converters
{
    public class FloatConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            // return an invalid value in case of the value ends with a point
            if (targetType == typeof(double))
            {
                return value.ToString().EndsWith(".") ? "." : value;
            }
            else
            {
                return value;
            }
        }
    }
}
