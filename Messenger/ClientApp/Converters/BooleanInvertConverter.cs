using System;
using System.Globalization;

namespace ClientApp.Converters
{
    public class BooleanInvertConverter : BaseValueConverter<BooleanInvertConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) => !(Boolean)value;

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
