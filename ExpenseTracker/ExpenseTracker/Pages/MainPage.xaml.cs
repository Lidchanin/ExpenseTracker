using ExpenseTracker.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage
    {
        public MainPage()
        {
            InitializeComponent();

            MasterPage.ListView.ItemSelected += OnItemSelected;
        }

        private void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (!(e.SelectedItem is MasterPageItem item))
                return;

            Detail = new NavigationPage((Page) Activator.CreateInstance(item.TargetType));
            MasterPage.ListView.SelectedItem = null;
            IsPresented = false;
        }
    }
}