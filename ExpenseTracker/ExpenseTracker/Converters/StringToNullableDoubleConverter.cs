using System;
using System.Globalization;
using Xamarin.Forms;

namespace ExpenseTracker.Converters
{
    public class StringToNullableDoubleConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (double?) value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                return null;

            var stringValue = value as string;

            if (string.IsNullOrEmpty(stringValue))
                return null;

            if (double.TryParse(stringValue, out var dbl))
                return dbl;

            return null;
        }
    }
}