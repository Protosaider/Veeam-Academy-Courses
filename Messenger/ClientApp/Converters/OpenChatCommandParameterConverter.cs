using System;
using System.Globalization;
using ClientApp.DataSuppliers.Data;
using ClientApp.Other;

namespace ClientApp.Converters
{
    public sealed class OpenChatCommandParameterConverter : BaseMultiValueConverter<OpenChatCommandParameterConverter>
    {
        public override Object Convert(Object[] values, Type targetType, Object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 4)
            //if (values != null && values.Length == 5)
            {
                //return new OpenChatCommandArgs(new CChatData((Guid)values[0], values[1].ToString(), (EChatType)values[2], (Boolean)values[3]), (Boolean)values[4]);
                return new CChatData((Guid)values[0], values[1].ToString(), (EChatType)values[2], (Boolean)values[3]);
            }
            return null;
        }

        public override Object[] ConvertBack(Object value, Type[] targetTypes, Object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
