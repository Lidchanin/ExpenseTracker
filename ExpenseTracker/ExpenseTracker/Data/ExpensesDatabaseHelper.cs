using ExpenseTracker.Data.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Data
{
    public class ExpensesDatabaseHelper : IExpenseDatabaseHelper
    {
        #region Instance

        private static ExpensesDatabaseHelper _instance;

        public static readonly ExpensesDatabaseHelper
            Instance = _instance ?? (_instance = new ExpensesDatabaseHelper());

        #endregion Instance

        public async Task<List<CategoryWithCostSum>> GetCategoriesWithCostSumAsync()
        {
            //SELECT DISTINCT c.Id as CategoryId,
            //                c.Name as CategoryName,
            //                ci.FilenameOrFilepath as File,
            //                c.HexColor as HexColor,
            //                sum(e.Cost) as TotalCost
            //FROM    Categories c
            //        INNER JOIN Expenses e ON e.CategoryId = c.Id
            //        INNER JOIN CategoryIcons ci ON ci.Id = c.CategoryIconId
            //GROUP BY c.Id
            //ORDER BY TotalCost DESC
            using (var dbContext = new ExpensesDatabaseContext())
            {
                return await ExecuteWithGeneralExceptionHandling(async () => await (
                        from category in dbContext.Categories
                        join expense in dbContext.Expenses
                            on category.Id equals expense.CategoryId
                        join categoryIcon in dbContext.CategoryIcons
                            on category.CategoryIconId equals categoryIcon.Id
                        group new
                        {
                            category.Id,
                            category.Name,
                            File = categoryIcon.FilenameOrFilepath,
                            category.HexColor,
                            expense.Cost
                        } by category.Id
                        into grouping
                        select new CategoryWithCostSum()
                        {
                            Id = grouping.FirstOrDefault().Id,
                            Name = grouping.FirstOrDefault().Name,
                            File = grouping.FirstOrDefault().File,
                            HexColor = grouping.FirstOrDefault().HexColor,
                            TotalSum = grouping.Sum(g => g.Cost)
                        })
                    .OrderByDescending(r => r.TotalSum)
                    .ToListAsync());
            }
        }

        public async Task<List<CategoryWithCostSum>> GetCategoriesWithCostSumAsync(DateTime startDate, DateTime endDate)
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
            using (var dbContext = new ExpensesDatabaseContext())
            {
                return await ExecuteWithGeneralExceptionHandling(async () => await (
                        from category in dbContext.Categories
                        join expense in dbContext.Expenses
                            on category.Id equals expense.CategoryId
                        join categoryIcon in dbContext.CategoryIcons
                            on category.CategoryIconId equals categoryIcon.Id
                        where expense.Timestamp >= startDate &&
                              expense.Timestamp <= endDate
                        group new
                        {
                            category.Id,
                            category.Name,
                            File = categoryIcon.FilenameOrFilepath,
                            category.HexColor,
                            expense.Cost
                        } by category.Id
                        into grouping
                        select new CategoryWithCostSum()
                        {
                            Id = grouping.FirstOrDefault().Id,
                            Name = grouping.FirstOrDefault().Name,
                            File = grouping.FirstOrDefault().File,
                            HexColor = grouping.FirstOrDefault().HexColor,
                            TotalSum = grouping.Sum(g => g.Cost)
                        })
                    .OrderByDescending(r => r.TotalSum)
                    .ToListAsync());
            }
        }

        #region Private methods

        private static async Task<T> ExecuteWithGeneralExceptionHandling<T>(Func<Task<T>> func)
        {
            try
            {
                return await func();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion Private methods
    }
}