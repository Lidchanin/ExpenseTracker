using ExpenseTracker.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public interface IExpenseDatabaseHelper
    {
        Task<List<CategoryWithCostSum>> GetCategoriesWithCostSumAsync();

        Task<List<CategoryWithCostSum>> GetCategoriesWithCostSumAsync(DateTime startDate, DateTime endDate);
    }
}