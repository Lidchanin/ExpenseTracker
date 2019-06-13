using ExpenseTracker.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddExpensePopup
    {
        public AddExpensePopup() =>
            InitializeComponent();

        protected override bool OnBackButtonPressed()
        {
            PageClosedTaskCompletionSource.SetResult(null);
            return base.OnBackButtonPressed();
        }

        protected override bool OnBackgroundClicked()
        {
            PageClosedTaskCompletionSource.SetResult(null);
            return base.OnBackgroundClicked();
        }

        private void OkButton_OnClicked(object sender, EventArgs e) =>
            PageClosedTaskCompletionSource.SetResult(new Expense
            {
                Name = ViewModel.Name,
                Cost = ViewModel.Cost ?? 0,
                Timestamp = ViewModel.Timestamp,
                CategoryId = ViewModel.Category.Id
            });

        private void CancelButton_OnClicked(object sender, EventArgs e) =>
            PageClosedTaskCompletionSource.SetResult(null);
    }
}