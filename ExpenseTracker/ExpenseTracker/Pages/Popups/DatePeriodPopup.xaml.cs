using System;
using ExpenseTracker.Enums;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DatePeriodPopup
	{
        public Task<DatePeriods> PageClosedTask => PageClosedTaskCompletionSource.Task;

        private TaskCompletionSource<DatePeriods> PageClosedTaskCompletionSource { get; } =
            new TaskCompletionSource<DatePeriods>();

        public DatePeriodPopup ()
		{
			InitializeComponent ();
        }

        protected override bool OnBackButtonPressed()
        {
            PageClosedTaskCompletionSource.SetResult(DatePeriods.None);
            return base.OnBackButtonPressed();
        }

        protected override bool OnBackgroundClicked()
        {
            PageClosedTaskCompletionSource.SetResult(DatePeriods.None);
            return base.OnBackgroundClicked();
        }

        //private void ListView_OnItemSelected(object sender, SelectedItemChangedEventArgs e) =>
        //    PageClosedTaskCompletionSource.SetResult((DatePeriods) e.SelectedItem);

        private void PeriodButtons_OnClicked(object sender, EventArgs e)
        {
            PageClosedTaskCompletionSource.SetResult((DatePeriods) ((Button) sender).CommandParameter);
        }
    }
}