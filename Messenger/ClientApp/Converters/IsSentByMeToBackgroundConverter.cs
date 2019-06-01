using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp.Converters
{
    public class IsSentByMeToBackgroundConverter : BaseValueConverter<IsSentByMeToBackgroundConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            return (Boolean)value ? Application.Current.FindResource("VeryLightBlueBrush") : Application.Current.FindResource("ForegroundLightBrush");
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
