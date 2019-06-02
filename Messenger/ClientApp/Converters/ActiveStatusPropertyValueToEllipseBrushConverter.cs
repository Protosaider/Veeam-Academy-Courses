using System;
using System.Globalization;
using System.Windows;
using System.Windows.Media;

namespace ClientApp.Converters
{

    public sealed class ActivityStatusPropertyValueToEllipseBrushConverter : BaseValueConverter<ActivityStatusPropertyValueToEllipseBrushConverter>
    {
        private readonly SolidColorBrush _activityEqualsZeroBrush =
            (SolidColorBrush)Application.Current.Resources["ForegroundDarkBrush"];
        private readonly SolidColorBrush _activityEqualsOneBrush =
            (SolidColorBrush)Application.Current.Resources["BlueBrush"];

        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return value != null && (Int32)value == 0 ? _activityEqualsZeroBrush : _activityEqualsOneBrush;
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
