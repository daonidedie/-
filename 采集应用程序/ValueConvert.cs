using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace 采集应用程序
{
   public class ValueConvert:System.Windows.Data.IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (!(bool)value);

        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return (!(bool)value);
        }
    }
}
