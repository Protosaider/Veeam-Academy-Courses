using System;
using System.Globalization;
using System.Windows;

namespace ClientApp.Converters
{
    public class BooleanToVisibilityInverseValueConverter : BaseValueConverter<BooleanToVisibilityInverseValueConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (Boolean)value ? Visibility.Visible : Visibility.Hidden;
            else
                return (Boolean)value ? Visibility.Hidden : Visibility.Visible;
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
