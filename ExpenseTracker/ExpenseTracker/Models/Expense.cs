using System;

namespace ExpenseTracker.Models
{
    public class Expense
    {
        public long Id { get; set; }

        public string Name { get; set; }

        public double Cost { get; set; }

        public DateTime Timestamp { get; set; }

        public long CategoryId { get; set; }

        public Category Category { get; set; }
    }
}