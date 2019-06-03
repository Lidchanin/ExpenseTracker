using ExpenseTracker.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public abstract class BasePage : ContentPage
    {
        protected PopupService PopupService = PopupService.Instance;

        /*private readonly Grid _mainGrid = new Grid();

        public Layout<View> ContentWithOverlay
        {
            get => _mainGrid;
            set
            {
                _mainGrid.Children.Add(value);
                Content = _mainGrid;
            }
        }

        protected void OverlayParent(View view) => 
            _mainGrid.Children.Add(view);

        protected void RemoveLastPopup()
        {
            if (_mainGrid.Children.Count > 1)
                _mainGrid.Children.RemoveAt(1);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            RemoveLastPopup();
        }*/
    }
}