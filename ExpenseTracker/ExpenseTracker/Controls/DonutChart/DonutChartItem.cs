namespace ExpenseTracker.Controls.DonutChart
{
    public class DonutChartItem
    {
        public DonutChartItem(float value, string sectionHexColor)
        {
            Value = value;
            SectionHexColor = sectionHexColor;
        }

        public float Value { get; }

        public string PrimaryText { get; set; }

        public string SecondaryText { get; set; }

        public string SectionHexColor { get; set; }
    }
}