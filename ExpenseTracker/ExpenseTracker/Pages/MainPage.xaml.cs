using ExpenseTracker.Helpers;

namespace ExpenseTracker.Pages
{
    public partial class MainPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            var helper = new ExpensesDatabaseHelper();
        }
    }
}
