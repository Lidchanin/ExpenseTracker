using SkiaSharp;

namespace ExpenseTracker.Controls.DonutChart
{
    public class DonutChartItem
    {
        public DonutChartItem(float value, string sectionHexColor, SKBitmap bitmap = null)
        {
            Value = value;
            SectionHexColor = sectionHexColor;
            Bitmap = bitmap;
        }

        public float Value { get; }

        public string SectionHexColor { get; }

        public SKBitmap Bitmap { get; }
    }
}