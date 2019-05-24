namespace ExpenseTracker.Data.DTOs
{
    public class CategoryWithCostSum
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string HexColor { get; set; }

        public string File { get; set; }

        public double TotalSum { get; set; }
    }
}