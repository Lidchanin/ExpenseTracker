using ExpenseTracker.Helpers;
using System.Collections.Generic;

namespace ExpenseTracker.Models
{
    public class CategoryIcon
    {
        public long Id { get; set; }

        public string FilenameOrFilepath { get; set; } = ConstantHelper.PlaceholderIcon;

        public ICollection<Category> Categories { get; set; }
    }
}