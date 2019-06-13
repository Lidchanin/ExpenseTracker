using ExpenseTracker.Data.DTOs;
using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public class ExpensesRepository : IExpenseRepository
    {
        #region Instance

        private static readonly ExpensesRepository _instance;

        public static readonly ExpensesRepository
            Instance = _instance ?? (_instance = new ExpensesRepository());

        #endregion Instance

        public async Task<List<CategoryWithCostSum>> GetCategoriesWithCostSumAsync(
            DateTime fromDate,
            DateTime toDate)
        {
            //SELECT DISTINCT c.Id as CategoryId,
            //                c.Name as CategoryName,
            //                ci.FilenameOrFilepath as File,
            //                c.HexColor as HexColor,
            //                sum(e.Cost) as TotalCost
            //FROM    Categories c
            //        INNER JOIN Expenses e ON e.CategoryId = c.Id
            //        INNER JOIN CategoryIcons ci ON ci.Id = c.CategoryIconId
            //WHERE   e.Timestamp >= '2019-03-01 00:00:00' AND e.Timestamp <= '2019-04-01 00:00:00'
            //GROUP BY c.Id
            //ORDER BY TotalCost DESC
            using (var dbContext = new ExpensesDbContext())
            {
                return await ExecuteWithGeneralExceptionHandling(async () =>
                {
                    var categoriesWithCostSum = await (
                            from category in dbContext.Categories
                            join expense in dbContext.Expenses
                                on category.Id equals expense.CategoryId
                            join categoryIcon in dbContext.CategoryIcons
                                on category.CategoryIconId equals categoryIcon.Id
                            where expense.Timestamp >= fromDate &&
                                  expense.Timestamp <= toDate
                            group new
                            {
                                category.Id,
                                category.Name,
                                File = categoryIcon.FilenameOrFilepath,
                                category.HexColor,
                                expense.Cost
                            } by category.Id
                            into grouping
                            select new CategoryWithCostSum
                            {
                                Id = grouping.FirstOrDefault().Id,
                                Name = grouping.FirstOrDefault().Name,
                                File = grouping.FirstOrDefault().File,
                                HexColor = grouping.FirstOrDefault().HexColor,
                                TotalSum = grouping.Sum(g => g.Cost)
                            })
                        .OrderByDescending(r => r.TotalSum)
                        .ToListAsync();

                    return categoriesWithCostSum;
                });
            }
        }

        public async Task<List<Category>> GetCategoriesAsync()
        {
            using (var dbContext = new ExpensesDbContext())
            {
                return await ExecuteWithGeneralExceptionHandling(async () =>
                {
                    var categories = await dbContext.Categories.ToListAsync();

                    return categories;
                });
            }
        }

        //todo [?] The task result contains the number of state entries written to the database.
        public async Task<Expense> AddExpenseAsync(Expense expense)
        {
            using (var dbContext = new ExpensesDbContext())
            {
                return await ExecuteWithGeneralExceptionHandling(async () =>
                {
                    var entityExpense = await dbContext.AddAsync(expense);
                    var dbExpense = entityExpense.Entity;
                    var numberOfRecords = await dbContext.SaveChangesAsync();

                    return dbExpense;
                });
            }
        }

        #region Private methods

        private static async Task<T> ExecuteWithGeneralExceptionHandling<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            //todo [!] Need more exceptions
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion Private methods
    }
}