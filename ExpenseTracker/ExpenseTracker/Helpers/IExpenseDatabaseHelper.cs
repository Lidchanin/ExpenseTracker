using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ExpenseTracker.Helpers
{
    /// <summary>
    /// GUID operations
    /// </summary>
    public interface IExpenseDatabaseHelper
    {
        Task<List<Category>> GetCategoriesAsync();

        //Task<List<Category>> GetCategoriesAsync(DateTime startDate, DateTime endDate);

        Task<List<Category>> GetCategoriesWithExpensesAsync();

        //Task<List<Category>> GetCategoriesWithExpensesAsync(DateTime startDate, DateTime endDate);


        //Task InsertCategoryAsync(Category category);

        //Task InsertExpenseAsync(Expense expense);


        //Task UpdateCategoryAsync(Category category);

        //Task UpdateExpenseAsync(Expense expense);


        //Task DeleteCategoryAsync(Category category);

        //Task DeleteCategoryAsync(long categoryId);

        //Task DeleteExpenseAsync(Expense expense);

        //Task DeleteExpenseAsync(long expenseId);
    }
}