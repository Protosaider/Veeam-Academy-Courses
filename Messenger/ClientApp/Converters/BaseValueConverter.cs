using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace ClientApp.Converters
{
    public abstract class BaseValueConverter<T> : MarkupExtension, IValueConverter where T : class, new()
    {
        private static T s_converter;

        #region MarkupExtension
        public override Object ProvideValue(IServiceProvider serviceProvider)
        {
            return s_converter ?? (s_converter = new T());
        }
        #endregion

        #region IValueConverter
        public abstract Object Convert(Object value, Type targetType, Object parameter, CultureInfo culture);
        public abstract Object ConvertBack(Object value, Type targetType, Object parameter, CultureInfo culture);
        #endregion
    }

    public abstract class BaseMultiValueConverter<T> : MarkupExtension, IMultiValueConverter where T : class, new()
    {
        private static T s_converter;

        #region MarkupExtension
        public override Object ProvideValue(IServiceProvider serviceProvider)
        {
            return s_converter ?? (s_converter = new T());
        }
        #endregion

        #region IValueConverter
        public abstract Object Convert(Object[] values, Type targetType, Object parameter, CultureInfo culture);
        public abstract Object[] ConvertBack(Object value, Type[] targetTypes, Object parameter, CultureInfo culture);
        #endregion
    }
}
