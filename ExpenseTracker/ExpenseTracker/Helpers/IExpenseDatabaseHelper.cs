using ExpenseTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Helpers
{
    public interface IExpenseDatabaseHelper
    {
        #region Get

        //todo [TestMethod]
        Task<List<Category>> GetDataAsync();
        //todo [TestMethod]
        Task<List<Expense>> GetDataAsync(DateTime startDate, DateTime endTime);
        //todo [TestMethod]
        Task<List<IGrouping<Category, Expense>>> GetDataForMonthAsync(DateTime currentDateTime);

        #endregion Get

        #region Insert

        Task InsertCategoryAsync(Category category);
        Task InsertExpenseAsync(Expense expense);

        #endregion Insert

        #region Update

        #endregion Update

        #region Delete

        #endregion Delete
    }
}