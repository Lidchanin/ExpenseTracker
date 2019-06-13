using System;
using ExpenseTracker.Helpers;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    [QueryProperty("CategoryId", RoutingHelper.CategoryId)]
    public partial class CategoryDetailsPage
    {
        private string _categoryId;

        public string CategoryId
        {
            get => _categoryId;
            set => _categoryId = Uri.UnescapeDataString(value);
        }

        public CategoryDetailsPage()
        {
            InitializeComponent();
        }
    }
}