using System;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StartPage
	{
		public StartPage()
		{
			InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }
    }
}