using System;
using System.Globalization;
using System.Windows.Data;

namespace JClock
{
    public class FontConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            
            Double result = (int.Parse(value.ToString()) / 1)*(32.0/50) ; 
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}