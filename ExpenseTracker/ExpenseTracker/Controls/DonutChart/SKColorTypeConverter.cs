using SkiaSharp;
using System;
using Xamarin.Forms;

namespace ExpenseTracker.Controls.DonutChart
{
    public class SKColorTypeConverter : TypeConverter
    {
        public override bool CanConvertFrom(Type sourceType)
        {
            return sourceType == typeof(string);
        }

        public override object ConvertFromInvariantString(string value)
        {
            var hexColor = value;

            if (SKColor.TryParse(hexColor, out var result))
                return result;

            throw new ArgumentException();
        }
    }
}