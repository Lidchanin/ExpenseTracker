using ExpenseTracker.Helpers;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomeListPage
    {
        public HomeListPage()
        {
            InitializeComponent();

            BindingContext = ViewModelLocator.HomeViewModel;
        }
    }
}