using System;
using System.Globalization;
using System.Windows;

namespace ClientApp.Converters
{
    public sealed class BooleanToVisibilityInverseValueConverter : BaseValueConverter<BooleanToVisibilityInverseValueConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
		{
			if (parameter == null)
                return value != null && (Boolean)value ? Visibility.Visible : Visibility.Hidden;

			return value != null && (Boolean)value ? Visibility.Hidden : Visibility.Visible;
		}

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
