using System;
using System.Globalization;

namespace ClientApp.Converters
{
    public sealed class TimeToLastActiveTimeConverter : BaseValueConverter<TimeToLastActiveTimeConverter>
    {
        private readonly String _prefix = "Last active: ";
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
		{
			// Get the time passed in
			if (value != null)
			{
				var time = (DateTimeOffset)value;

				// If it is today...
				if (time.Date == DateTimeOffset.UtcNow.Date)
					// Return just time
					return _prefix + time.ToLocalTime().ToString("HH:mm");

				// Otherwise, return a full date
				return _prefix + time.ToLocalTime().ToString("HH:mm, MMM yyyy");
			}

			return "Null date";
		}

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
