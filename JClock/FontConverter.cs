using System;
using System.Globalization;
using System.Windows.Data;

namespace JClock
{
    public class FontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            Double rate = Double.Parse(parameter.ToString());
            Double result = (double.Parse(value.ToString(),CultureInfo.CurrentUICulture) / 1)*(30.0/50)* (rate/100); 
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}