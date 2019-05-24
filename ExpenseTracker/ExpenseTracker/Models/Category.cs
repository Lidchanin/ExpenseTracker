using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    public class Category
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public string HexColor { get; set; }

        public ICollection<Expense> Expenses { get; set; }

        public long CategoryIconId { get; set; }

        public CategoryIcon CategoryIcon { get; set; }

        public override string ToString() => Name;
    }
}