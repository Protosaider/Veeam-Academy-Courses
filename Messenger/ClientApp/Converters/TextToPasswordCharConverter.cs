using System;
using System.Globalization;

namespace ClientApp.Converters
{
    public sealed class TextToPasswordCharConverter : BaseValueConverter<TextToPasswordCharConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
		{
			return value != null ? new String('\uf111', ((String)value).Length) : String.Empty;
		}

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
