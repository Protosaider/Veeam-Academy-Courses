using System;
using System.Globalization;

namespace ClientApp.Converters
{
    public sealed class TimeToDisplayTimeConverter : BaseValueConverter<TimeToDisplayTimeConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            // Get the time passed in
			if (value != null)
			{
				var time = (DateTimeOffset)value;

                // If it is today... Return just time Otherwise, return a full date
				return time.ToLocalTime().ToString(time.Date == DateTimeOffset.UtcNow.Date ? "HH:mm" : "HH:mm, MMM yyyy"); 
			}
			//Debugger.Break();
			return "Null date";
		}

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
