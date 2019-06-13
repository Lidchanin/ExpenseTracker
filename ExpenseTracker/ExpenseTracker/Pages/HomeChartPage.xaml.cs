using ExpenseTracker.Helpers;
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
    }
}