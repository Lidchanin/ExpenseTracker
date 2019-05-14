using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace ExpenseTracker.Helpers
{
    public class ExpensesDatabaseHelper : ExpensesDatabaseContext, IExpenseDatabaseHelper
    {
        

        #region Private methods

        private static async Task<ExpensesDatabaseContext> CreateContextAsync()
        {
            var dbContext = new ExpensesDatabaseContext();
            await dbContext.Database.EnsureCreatedAsync();
            return dbContext;
        }

        #endregion Private methods

        
    }
}