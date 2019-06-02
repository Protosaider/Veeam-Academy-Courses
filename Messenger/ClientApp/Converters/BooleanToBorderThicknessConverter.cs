using System;
using System.Globalization;

namespace ClientApp.Converters
{
    public sealed class BooleanToBorderThicknessConverter : BaseValueConverter<BooleanToBorderThicknessConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
		{
			if (parameter == null)
                return value != null && (Boolean)value ? 2 : 0;

			return value != null && (Boolean)value ? 0 : 2;
		}

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
