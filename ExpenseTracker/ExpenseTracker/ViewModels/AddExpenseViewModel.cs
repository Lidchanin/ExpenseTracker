using ExpenseTracker.Models;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.ViewModels
{
    public class AddExpenseViewModel : BaseViewModel
    {
        private double? _cost;

        public double? Cost
        {
            get => _cost;
            set
            {
                if (_cost == value)
                    return;

                _cost = value;
                IsExpenseValid = CheckIsExpenseValid();
            }
        }

        public string Name { get; set; }

        public DateTime Timestamp { get; set; }

        private Category _category;

        public Category Category
        {
            get => _category;
            set
            {
                if (_category == value)
                    return;

                _category = value;
                IsExpenseValid = CheckIsExpenseValid();
            }
        }

        public List<Category> Categories { get; set; }

        public bool IsExpenseValid { get; set; }

        public AddExpenseViewModel()
        {
            Timestamp = DateTime.Today;
            IsExpenseValid = false;

            InitData();
        }

        #region Private methods

        private async void InitData()
        {
            Categories = await ExpenseRepository.GetCategoriesAsync();
        }

        private bool CheckIsExpenseValid() =>
            _cost != null && _cost > 0 && _category != null;

        #endregion Private methods
    }
}