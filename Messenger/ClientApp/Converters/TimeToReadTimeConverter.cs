using System;
using System.Globalization;

namespace ClientApp.Converters
{
    public class TimeToReadTimeConverter : BaseValueConverter<TimeToReadTimeConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
		{
			if (value == null)
				return "Null date";

            // Get the time passed in
            var time = (DateTimeOffset)value;

            // If it is not read...
            if (time == DateTimeOffset.MinValue)
                // Show nothing
                return String.Empty;

            // If it is today... Return just time Otherwise, return a full date
            return time.Date == DateTimeOffset.UtcNow.Date ? $"Read {time.ToLocalTime():HH:mm}" : $"Read {time.ToLocalTime():HH:mm, MMM yyyy}";
		}

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
