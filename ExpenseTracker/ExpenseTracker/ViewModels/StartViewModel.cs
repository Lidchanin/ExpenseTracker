using System;
using System.Collections.Generic;
using System.Windows.Input;
using ExpenseTracker.Models;
using Xamarin.Forms;

namespace ExpenseTracker.ViewModels
{
    public class StartViewModel : BaseViewModel
    {
        public string SelectedDatePeriod { get; set; } = DateTime.Today.ToShortDateString();
        public List<Category> Categories { get; set; }

        public string CategoryName { get; set; }
        public long CategoryIcId { get; set; }

        public string ExpenseName { get; set; }
        public double ExpenseCost { get; set; }
        public long ExpenseCatId { get; set; }
        public DateTime ExpenseDate { get; set; } = DateTime.Today;

        public ICommand AddExpenseCommand { get; set; }
        public ICommand AddCategoryCommand { get; set; }

        public StartViewModel()
        {
            InitData();

            AddExpenseCommand = new Command(async () =>
                await ExpenseDatabaseHelper.InsertExpenseAsync(new Expense
                {
                    Name = ExpenseName,
                    Cost = ExpenseCost,
                    CategoryId = ExpenseCatId,
                    Timestamp = ExpenseDate
                }));

            AddCategoryCommand = new Command(async () =>
                await ExpenseDatabaseHelper.InsertCategoryAsync(new Category
                {
                    CategoryIconId = CategoryIcId,
                    Name = CategoryName
                })
            );
        }

        #region Private methods

        private async void InitData()
        {
            Categories = await ExpenseDatabaseHelper.GetDataAsync();
        }

        #endregion Private methods
    }
}