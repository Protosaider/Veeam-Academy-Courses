using DTO;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace ClientApp.Converters
{
    public class LogInCommandParameterConverter : BaseMultiValueConverter<LogInCommandParameterConverter>
    {
        public override Object Convert(Object[] values, Type targetType, Object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 2)
            {
                //String login = values[0].ToString();
                //String password = values[1].ToString();

                return new List<String>(2) { values[0]?.ToString(), values[1]?.ToString() };
            }
            return null;
        }

        public override Object[] ConvertBack(Object value, Type[] targetTypes, Object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
