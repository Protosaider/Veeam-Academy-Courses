using ClientApp.Other;
using System;
using System.Globalization;
using System.Windows;

namespace ClientApp.Converters
{
    /// <summary>
    /// A converter that takes in a <see cref="IconType"/> and returns 
    /// the FontAwesome string for that icon
    /// </summary>
    public class IsPersonalPropertyValueToFontAwesomeConverter : BaseValueConverter<IsPersonalPropertyValueToFontAwesomeConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return (Boolean)value ? "\uf3ed" : String.Empty; //Shield icon
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
