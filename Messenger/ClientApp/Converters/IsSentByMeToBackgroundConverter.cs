using System;
using System.Globalization;
using System.Windows;

namespace ClientApp.Converters
{
    public sealed class IsSentByMeToBackgroundConverter : BaseValueConverter<IsSentByMeToBackgroundConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return (value != null && (Boolean)value) ? Application.Current.FindResource("VeryLightBlueBrush") : Application.Current.FindResource("ForegroundLightBrush");
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
