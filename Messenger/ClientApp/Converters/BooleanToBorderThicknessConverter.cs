using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientApp.Converters
{
    public class BooleanToBorderThicknessConverter : BaseValueConverter<BooleanToBorderThicknessConverter>
    {
        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            if (parameter == null)
                return (Boolean)value ? 2 : 0;
            else
                return (Boolean)value ? 0 : 2;
        }

        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
