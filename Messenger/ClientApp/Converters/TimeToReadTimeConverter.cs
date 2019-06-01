using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp.Converters
{
    public class TimeToReadTimeConverter : BaseValueConverter<TimeToReadTimeConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            // Get the time passed in
            var time = (DateTimeOffset)value;

            // If it is not read...
            if (time == DateTimeOffset.MinValue)
                // Show nothing
                return String.Empty;

            // If it is today...
            if (time.Date == DateTimeOffset.UtcNow.Date)
                // Return just time
                return $"Read {time.ToLocalTime().ToString("HH:mm")}";

            // Otherwise, return a full date
            return $"Read {time.ToLocalTime().ToString("HH:mm, MMM yyyy")}";
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
