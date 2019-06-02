using System;
using System.Globalization;

using System.Windows.Media;

namespace ClientApp.Converters
{
    public sealed class StringRGBToBrushConverter : BaseValueConverter<StringRGBToBrushConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (value == null)
                return (SolidColorBrush)(new BrushConverter().ConvertFrom(@"#3099c5"));
            return (SolidColorBrush)(new BrushConverter().ConvertFrom($"#{value}"));
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
