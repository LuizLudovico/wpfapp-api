using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp.ViewModels
{
    public class DateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is DateTime dateTime && dateTime != DateTime.MinValue)
            {
                return dateTime.ToString("dd/MM/yyyy");
            }
            return string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string dateString && !string.IsNullOrWhiteSpace(dateString))
            {
                DateTime result;
                if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", 
                    CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                {
                    return result;
                }
            }
            return DateTime.MinValue;
        }
    }
}

