using System;
using System.Globalization;
using System.Windows;

namespace ClientApp.Converters
{
    public sealed class IsSentByMeToAlignmentConverter : BaseValueConverter<IsSentByMeToAlignmentConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
		{
			if (parameter == null)
                return (value != null && (Boolean)value) ? HorizontalAlignment.Right : HorizontalAlignment.Left;

			return (value != null && (Boolean)value) ? HorizontalAlignment.Left : HorizontalAlignment.Right;
		}

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
