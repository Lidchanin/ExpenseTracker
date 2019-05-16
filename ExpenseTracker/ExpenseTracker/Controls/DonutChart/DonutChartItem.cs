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

        public SKColor PrimaryTextColor { get; set; } = SKColors.Black;

        public SKColor SecondaryTextColor { get; set; } = SKColors.DarkSlateGray;
    }
}