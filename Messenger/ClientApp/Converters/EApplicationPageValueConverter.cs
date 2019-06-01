using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WpfLessons.Core.DataModels;
using WpfLessons.Pages;

//namespace WpfLessons.ValueConverters
//{
//    public class EApplicationPageValueConverter : BaseValueConverter<EApplicationPageValueConverter>
//    {
//        public override Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture)
//        {
//            switch ((EApplicationPage)value)
//            {
//                case EApplicationPage.Login:
//                    return new LogInPage();
//                case EApplicationPage.Register:
//                    return new SignUpPage();
//                case EApplicationPage.Chat:
//                    return new ChatPage();
//                default:
//                    Debugger.Break();
//                    return null;
//            }
//        }

//        public override Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
