using DTO;
using System;
using System.Collections.Generic;
using System.Globalization;
using ClientApp.DataSuppliers.Data;
using ClientApp.Other;
using ClientApp.ViewModels.ChatPage;

namespace ClientApp.Converters
{
    public class SendMessageCommandParameterConverter : BaseMultiValueConverter<SendMessageCommandParameterConverter>
    {
        public override Object Convert(Object[] values, Type targetType, Object parameter, CultureInfo culture)
        {
            if (values != null && values.Length == 4)
            {
                if ((Guid)values[0] != default(Guid) && values[1] != null)
                    return new CCreateMessageData((Guid)values[0], values[1].ToString(), (Boolean)values[2] ? 1 : 0,
                        values[3] == null ? String.Empty : values[3].ToString());
            }
            return null;
        }

        public override Object[] ConvertBack(Object value, Type[] targetTypes, Object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}
