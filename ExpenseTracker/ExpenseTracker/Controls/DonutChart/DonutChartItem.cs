using SkiaSharp;

namespace ExpenseTracker.Controls.DonutChart
{
    public class DonutChartItem
    {
        public DonutChartItem(float value)
        {
            Value = value;
        }

        public float Value { get; }

        public string PrimaryText { get; set; }

        public string SecondaryText { get; set; }

        public SKColor SectionColor { get; set; } = SKColors.Black;
    }
}