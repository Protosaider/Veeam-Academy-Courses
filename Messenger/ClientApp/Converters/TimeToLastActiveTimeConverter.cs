using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp.Converters
{
    public class TimeToLastActiveTimeConverter : BaseValueConverter<TimeToLastActiveTimeConverter>
    {
        private readonly String _prefix = "Last active: ";
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            // Get the time passed in
            var time = (DateTimeOffset)value;

            // If it is today...
            if (time.Date == DateTimeOffset.UtcNow.Date)
                // Return just time
                return _prefix + time.ToLocalTime().ToString("HH:mm");

            // Otherwise, return a full date
            return _prefix + time.ToLocalTime().ToString("HH:mm, MMM yyyy");
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
