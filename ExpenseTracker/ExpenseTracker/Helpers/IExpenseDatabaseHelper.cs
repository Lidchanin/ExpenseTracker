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
        #region Get

        Task<List<Category>> GetCategoriesAsync();

        //Task<List<Category>> GetCategoriesAsync(DateTime startDate, DateTime endDate);

        Task<List<Category>> GetCategoriesWithExpensesAsync();

        //Task<List<Category>> GetCategoriesWithExpensesAsync(DateTime startDate, DateTime endDate);

        //Task<List<CategoryIcon>> GetCategoryIconsAsync();

        //Task<CategoryIcon> GetCategoryIconAsync(Category category);

        //Task<CategoryIcon> GetCategoryIconAsync(long categoryId);

        //Task<List<Expense>> GetExpensesAsync();

        //Task<List<Expense>> GetExpensesAsync(Category category);

        //Task<List<Expense>> GetExpensesAsync(Category category, DateTime startDate, DateTime endDate);

        //Task<List<Expense>> GetExpensesAsync(long categoryId);

        //Task<List<Expense>> GetExpensesAsync(long categoryId, DateTime startDate, DateTime endDate);

        #endregion Get

        #region Insert

        Task InsertCategoryAsync(Category category);

        Task InsertCategoryIconAsync(CategoryIcon categoryIcon);

        Task InsertExpenseAsync(Expense expense);

        #endregion Insert

        #region Update

        //Task UpdateCategoryAsync(Category category);
        
        //Task UpdateCategoryIconAsync(CategoryIcon categoryIcon);

        //Task UpdateExpenseAsync(Expense expense);

        #endregion Update

        #region Delete

        //Task DeleteCategoryAsync(Category category);

        //Task DeleteCategoryAsync(long categoryId);

        //Task DeleteExpenseAsync(Expense expense);

        //Task DeleteExpenseAsync(long expenseId);
    
        #endregion Delete
    }
}