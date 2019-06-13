using Android.Content;
using Android.Views.InputMethods;
using ExpenseTracker.Controls;
using ExpenseTracker.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(BorderlessEntry), typeof(BorderlessEntryRenderer))]
namespace ExpenseTracker.Droid.Renderers
{
    public class BorderlessEntryRenderer : EntryRenderer
    {
        public BorderlessEntryRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Background = null;

                var layoutParams = new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.SetPadding(0, 0, 0, 0);
                SetPadding(0, 0, 0, 0);
            }

            if (e.NewElement != null &&
                !((BorderlessEntry) e.NewElement).IsKeyboardEnabled)
            {
                ((BorderlessEntry) e.NewElement).PropertyChanging += OnPropertyChanging;
                Control.ShowSoftInputOnFocus = false;
            }

            if (e.OldElement != null &&
                !((BorderlessEntry) e.OldElement).IsKeyboardEnabled)
            {
                ((BorderlessEntry) e.OldElement).PropertyChanging -= OnPropertyChanging;
            }
        }

        private void OnPropertyChanging(object sender, PropertyChangingEventArgs propertyChangingEventArgs)
        {
            if (propertyChangingEventArgs.PropertyName == VisualElement.IsFocusedProperty.PropertyName)
            {
                var imm = (InputMethodManager) Context.GetSystemService(Context.InputMethodService);
                imm.HideSoftInputFromWindow(Control.WindowToken, 0);
            }
        }
    }
}