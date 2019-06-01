using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientApp.Converters
{
    public class IsSentByMeToAlignmentConverter : BaseValueConverter<IsSentByMeToAlignmentConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (Boolean)value ? HorizontalAlignment.Right : HorizontalAlignment.Left;
            else
                return (Boolean)value ? HorizontalAlignment.Left : HorizontalAlignment.Right;
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
