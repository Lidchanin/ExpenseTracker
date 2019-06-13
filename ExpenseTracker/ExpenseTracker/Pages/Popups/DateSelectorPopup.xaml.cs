using System;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DateSelectorPopup
    {
        public DateSelectorPopup() =>
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
            PageClosedTaskCompletionSource.SetResult(
                new Tuple<DateTime, DateTime>(ViewModel.StartDate, ViewModel.EndDate));

        private void CancelButton_OnClicked(object sender, EventArgs e) => 
            PageClosedTaskCompletionSource.SetResult(null);
    }
}