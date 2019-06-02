using System;
using System.Globalization;
using System.Windows;

namespace ClientApp.Converters
{
    public sealed class IsSentByMeToPaddingConverter : BaseValueConverter<IsSentByMeToPaddingConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
		{
			if (parameter == null)
                return value != null && (Boolean)value ? new Thickness(15, 10, 10, 10) : new Thickness(10, 10, 15, 10);

			return value != null && (Boolean)value ? new Thickness(10, 10, 15, 10) : new Thickness(15, 10, 10, 10);
		}

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
