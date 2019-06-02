using System;
using System.Globalization;
using System.Windows;

namespace ClientApp.Converters
{
    public sealed class IsSentByMeToMarginConverter : BaseValueConverter<IsSentByMeToMarginConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
		{
			if (parameter == null)
                return (value != null && (Boolean)value) ? new Thickness(50, 0, 25, 0) : new Thickness(25, 0, 50, 0);

			return (value != null && (Boolean)value) ? new Thickness(25, 0, 50, 0) : new Thickness(50, 0, 25, 0);
		}

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
