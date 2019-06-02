using System;
using System.Globalization;

namespace ClientApp.Converters
{
    public sealed class BooleanInvertConverter : BaseValueConverter<BooleanInvertConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture) => value != null && !(Boolean)value;

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
