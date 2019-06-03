using System;
using System.ComponentModel;
using System.Globalization;
using Xamarin.Forms;

namespace ExpenseTracker.Converters
{
    public class EnumDescriptionConverter : IValueConverter
    {
        object IValueConverter.Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var myEnum = (Enum) value;

            if (myEnum == null)
                return null;

            var description = GetEnumDescription(myEnum);

            return !string.IsNullOrEmpty(description)
                ? description
                : myEnum.ToString();
        }

        object IValueConverter.ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            string.Empty;

        private static string GetEnumDescription(Enum enumObj)
        {
            if (enumObj == null)
                return string.Empty;

            var fieldInfo = enumObj.GetType().GetField(enumObj.ToString());

            var attributeArray = fieldInfo.GetCustomAttributes(false);

            if (attributeArray.Length == 0)
                return enumObj.ToString();

            var attribute = attributeArray[0] as DescriptionAttribute;
            return attribute?.Description;
        }
    }
}