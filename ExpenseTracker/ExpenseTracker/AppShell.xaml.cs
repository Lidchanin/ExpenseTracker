using ExpenseTracker.Helpers;
using ExpenseTracker.Pages;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AppShell
    {
        public AppShell()
        {
            InitializeComponent();

            BindingContext = this;

            RegisterRoutes();
        }

        private static void RegisterRoutes()
        {
            Routing.RegisterRoute(RoutingHelper.HomeChartCategoryDetails, typeof(CategoryDetailsPage));
            Routing.RegisterRoute(RoutingHelper.HomeListCategoryDetails, typeof(CategoryDetailsPage));
        }
    }
}