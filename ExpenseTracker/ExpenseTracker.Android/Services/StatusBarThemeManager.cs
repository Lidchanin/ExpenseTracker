using Android.OS;
using Android.Views;
using ExpenseTracker.Droid.Services;
using ExpenseTracker.Services;
using Plugin.CurrentActivity;
using Xamarin.Forms;
using static Android.Graphics.Color;

[assembly: Dependency(typeof(StatusBarThemeManager))]

namespace ExpenseTracker.Droid.Services
{
    public class StatusBarThemeManager : IStatusBarThemeManager
    {
        public void SetOrangeRedTheme()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentWindow();
                    currentWindow.DecorView.SystemUiVisibility = (StatusBarVisibility) SystemUiFlags.LightStatusBar;
                    currentWindow.SetStatusBarColor(ParseColor("#c25e00"));
                });
            }
        }

        public void SetIndigoBlueTheme()
        {
            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                Device.BeginInvokeOnMainThread(() =>
                {
                    var currentWindow = GetCurrentWindow();
                    currentWindow.DecorView.SystemUiVisibility = 0;
                    currentWindow.SetStatusBarColor(ParseColor("#000051"));
                });
            }
        }

        #region Private methods

        private static Window GetCurrentWindow()
        {
            var activity = CrossCurrentActivity.Current.Activity;
            var window = activity.Window;

            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);

            return window;
        }

        #endregion Private methods
    }
}