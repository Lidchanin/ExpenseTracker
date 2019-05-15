﻿using ExpenseTracker.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace ExpenseTracker.Helpers
{
    public class ExpensesDatabaseHelper : IExpenseDatabaseHelper
    {
        #region Instance

        private static ExpensesDatabaseHelper _instance;

        public static readonly ExpensesDatabaseHelper
            Instance = _instance ?? (_instance = new ExpensesDatabaseHelper());


        #endregion Instance

        public async Task Temp()
        {
            using (var dbContext = await CreateContextAsync())
            {

                var c1 = new Category {Name = "cat1"};
                var c2 = new Category {Name = "cat2"};
                var c3 = new Category {Name = "cat3"};

                await dbContext.Categories.AddAsync(c1);
                await dbContext.Categories.AddAsync(c2);
                await dbContext.Categories.AddAsync(c3);
                await dbContext.SaveChangesAsync();

                var e1 = new Expense {Name = "e1-c1", Category = c1};
                var e2 = new Expense {Name = "e2-c1", Category = c1};
                var e3 = new Expense {Name = "e3-c1", Category = c1};

                var e4 = new Expense {Name = "e4-c2", Category = c2};
                var e5 = new Expense {Name = "e5-c2", Category = c2};

                var e6 = new Expense {Name = "e6-c3", Category = c3};

                await dbContext.Expenses.AddAsync(e1);
                await dbContext.Expenses.AddAsync(e2);
                await dbContext.Expenses.AddAsync(e3);
                await dbContext.Expenses.AddAsync(e4);
                await dbContext.Expenses.AddAsync(e5);
                await dbContext.Expenses.AddAsync(e6);
                await dbContext.SaveChangesAsync();

                Debug.WriteLine("++++++++++++++++++++++++++++++++");
                var expenses = await dbContext.Expenses.Include(e => e.Category).ToListAsync();
                foreach (var expense in expenses)
                {
                    Console.WriteLine(
                        $"ex {expense.Id}, {expense.Name}, {expense.CategoryId}, {expense.Category.Name}");
                }

                Debug.WriteLine("++++++++++++++++++++++++++++++++");

                Debug.WriteLine("++++++++++++++++++++++++++++++++");
                var categories = await dbContext.Categories.Include(c => c.Expenses).ToListAsync();
                foreach (var category in categories)
                {
                    Debug.WriteLine($"+++ cat = {category.Id} + {category.Name}");
                    foreach (var categoryExpense in category.Expenses)
                    {
                        Debug.WriteLine($"++++++ ex = {categoryExpense.Id} {categoryExpense.Name}");
                    }
                }

                Debug.WriteLine("++++++++++++++++++++++++++++++++");
            }
        }

        #region Get

        public async Task<List<Category>> GetCategoriesAsync()
        {
            using (var dbContext = await CreateContextAsync())
            {
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    var categories = new List<Category>();
                    try
                    {
                        categories = await dbContext.Categories.ToListAsync();

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    return categories;
                }
            }
        }

        public async Task<List<Category>> GetCategoriesWithExpensesAsync()
        {
            using (var dbContext = await CreateContextAsync())
            {
                using (var transaction = await dbContext.Database.BeginTransactionAsync())
                {
                    var categories = new List<Category>();
                    IQueryable<Category> qCategories;

                    try
                    {
                        categories = await dbContext.Categories.Include(c => c.Expenses).ToListAsync();

                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }

                    return categories;
                }
            }
        }

        #endregion Get

        #region Insert

        public async Task InsertCategoryAsync(Category category)
        {
            using (var dbContext = await CreateContextAsync())
            {
                try
                {
                    await dbContext.AddAsync(category);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task InsertCategoryIconAsync(CategoryIcon categoryIcon)
        {
            using (var dbContext = await CreateContextAsync())
            {
                try
                {
                    await dbContext.AddAsync(categoryIcon);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        public async Task InsertExpenseAsync(Expense expense)
        {
            using (var dbContext = await CreateContextAsync())
            {
                try
                {
                    await dbContext.AddAsync(expense);
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
            }
        }

        #endregion Insert

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