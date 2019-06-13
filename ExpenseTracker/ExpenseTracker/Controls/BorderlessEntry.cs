using Xamarin.Forms;

namespace ExpenseTracker.Controls
{
    public class BorderlessEntry : Entry
    {
        public static readonly BindableProperty IsKeyboardEnabledProperty = BindableProperty.Create(
            nameof(IsKeyboardEnabled),
            typeof(bool),
            typeof(BorderlessEntry),
            true);

        public bool IsKeyboardEnabled
        {
            get => (bool) GetValue(IsKeyboardEnabledProperty);
            set => SetValue(IsKeyboardEnabledProperty, value);
        }
    }
}