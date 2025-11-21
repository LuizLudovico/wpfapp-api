using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfApp.ViewModels
{
    public class CurrencyConverter : IValueConverter
    {
        private static readonly CultureInfo BrazilianCulture = new CultureInfo("pt-BR");

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is decimal decimalValue)
            {
                return decimalValue.ToString("C", BrazilianCulture);
            }
            
            if (value is double doubleValue)
            {
                return doubleValue.ToString("C", BrazilianCulture);
            }
            
            if (value is int intValue)
            {
                return intValue.ToString("C", BrazilianCulture);
            }

            return value?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                // Remove R$ e espaços
                stringValue = stringValue.Replace("R$", "").Trim();
                
                if (decimal.TryParse(stringValue, NumberStyles.Currency, BrazilianCulture, out decimal result))
                {
                    return result;
                }
            }

            return 0m;
        }
    }
}

