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
        }
    }
}