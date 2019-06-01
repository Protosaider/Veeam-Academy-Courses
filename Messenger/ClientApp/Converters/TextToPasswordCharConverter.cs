using System;
using System.Linq;
using System.Globalization;

namespace ClientApp.Converters
{
    public class TextToPasswordCharConverter : BaseValueConverter<TextToPasswordCharConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return new String('\uf111', ((String)value).Length);         
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
