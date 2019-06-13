using ExpenseTracker.Data.DTOs;
using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public interface IExpenseRepository
    {
        Task<List<CategoryWithCostSum>> GetCategoriesWithCostSumAsync(DateTime fromDate, DateTime toDate);

        Task<List<Category>> GetCategoriesAsync();

        Task<Expense> AddExpenseAsync(Expense expense);
    }
}