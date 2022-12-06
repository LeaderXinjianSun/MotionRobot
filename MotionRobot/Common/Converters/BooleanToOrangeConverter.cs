

using System.Globalization;
using System;
using System.Windows.Data;
using System.Windows.Media;

namespace MotionRobot.Common.Converters
{
    internal class BooleanToOrangeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool v1)
            {
                return v1 ? (SolidColorBrush)new BrushConverter().ConvertFrom("#FB7E00") : new SolidColorBrush(Colors.Gray);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return default(bool);
        }
    }
}
