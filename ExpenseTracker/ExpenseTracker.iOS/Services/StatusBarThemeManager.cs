using ExpenseTracker.iOS.Services;
using ExpenseTracker.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarThemeManager))]
namespace ExpenseTracker.iOS.Services
{
    public class StatusBarThemeManager : IStatusBarThemeManager
    {
        public void SetOrangeRedTheme()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.Default, false);
                GetCurrentViewController().SetNeedsStatusBarAppearanceUpdate();
            });
        }

        public void SetIndigoBlueTheme()
        {
            Device.BeginInvokeOnMainThread(() =>
            {
                UIApplication.SharedApplication.SetStatusBarStyle(UIStatusBarStyle.LightContent, false);
                GetCurrentViewController().SetNeedsStatusBarAppearanceUpdate();
            });
        }

        #region Private methods

        private static UIViewController GetCurrentViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            var vc = window.RootViewController;
            while (vc.PresentedViewController != null)
                vc = vc.PresentedViewController;
            return vc;
        }

        #endregion Private methods
    }
}