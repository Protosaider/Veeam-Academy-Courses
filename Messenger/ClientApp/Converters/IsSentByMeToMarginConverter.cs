using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp.Converters
{
    public class IsSentByMeToMarginConverter : BaseValueConverter<IsSentByMeToMarginConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (Boolean)value ? new Thickness(50, 0, 25, 0) : new Thickness(25, 0, 50, 0);
            else
                return (Boolean)value ? new Thickness(25, 0, 50, 0) : new Thickness(50, 0, 25, 0);
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
