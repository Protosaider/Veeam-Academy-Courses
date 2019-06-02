using ClientApp.Other;
using System;
using System.Globalization;

namespace ClientApp.Converters
{

    public sealed class ChatTypeToFontAwesomeIconConverter : BaseValueConverter<ChatTypeToFontAwesomeIconConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
		{
			if (value == null)
				return EChatType.Common;

            return ((EChatType)value).ToFontAwesome();
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
