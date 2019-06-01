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
    public class ChatTypeToFontAwesomeIconConverter : BaseValueConverter<ChatTypeToFontAwesomeIconConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return ((EChatType)value).ToFontAwesome();
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
