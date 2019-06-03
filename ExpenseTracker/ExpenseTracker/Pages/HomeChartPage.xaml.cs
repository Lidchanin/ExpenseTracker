using ExpenseTracker.Helpers;
using System;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeChartPage
    {
        public HomeChartPage()
        {
            InitializeComponent();

            BindingContext = ViewModelLocator.HomeViewModel;
        }

        private async void CalendarItem_OnClicked(object sender, EventArgs e)
        {
            var datePeriod = await PopupService.ShowDatePeriodPopupAsync();
        }
    }
}