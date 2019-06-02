using System;
using System.Globalization;

namespace ClientApp.Converters
{

    public sealed class IsPersonalPropertyValueToFontAwesomeConverter : BaseValueConverter<IsPersonalPropertyValueToFontAwesomeConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return value != null && (Boolean)value ? "\uf3ed" : String.Empty; //Shield icon
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
