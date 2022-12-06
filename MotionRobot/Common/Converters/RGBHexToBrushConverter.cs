
using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Media;

namespace MotionRobot.Common.Converters
{
    internal class RGBHexToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string v1)
            {
                return (SolidColorBrush)new BrushConverter().ConvertFrom(v1);
            }
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
