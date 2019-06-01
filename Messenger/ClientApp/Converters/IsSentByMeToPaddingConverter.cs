using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp.Converters
{
    public class IsSentByMeToPaddingConverter : BaseValueConverter<IsSentByMeToPaddingConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (Boolean)value ? new Thickness(15, 10, 10, 10) : new Thickness(10, 10, 15, 10);
            else
                return (Boolean)value ? new Thickness(10, 10, 15, 10) : new Thickness(15, 10, 10, 10);
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
