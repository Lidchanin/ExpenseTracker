using ExpenseTracker.Enums;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DatePeriodSelectorPopup
    {
        public DatePeriodSelectorPopup(DatePeriod datePeriod)
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            PageClosedTaskCompletionSource.SetResult(DatePeriod.None);
            return base.OnBackButtonPressed();
        }

        protected override bool OnBackgroundClicked()
        {
            PageClosedTaskCompletionSource.SetResult(DatePeriod.None);
            return base.OnBackgroundClicked();
        }

        private void PeriodButtons_OnClicked(object sender, EventArgs e) =>
            PageClosedTaskCompletionSource.SetResult((DatePeriod) ((Button) sender).CommandParameter);
    }
}