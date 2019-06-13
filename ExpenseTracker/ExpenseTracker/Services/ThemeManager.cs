using ExpenseTracker.Enums;
using ExpenseTracker.Helpers;
using ExpenseTracker.Resources.Themes;
using Xamarin.Forms;

namespace ExpenseTracker.Services
{
    public static class ThemeManager
    {
        public static void ChangeTheme(Theme theme)
        {
            //todo [?] Delete
            //if(theme == CurrentTheme())
            //    return;

            var mergedDictionaries = Application.Current.Resources.MergedDictionaries;
            mergedDictionaries.Clear();

            PreferencesHelper.SetTheme(theme);

            var statusBarStyleManager = DependencyService.Get<IStatusBarThemeManager>();

            switch (theme)
            {
                case Theme.IndigoBlue:
                    mergedDictionaries.Add(new IndigoBlueTheme());
                    statusBarStyleManager.SetIndigoBlueTheme();
                    break;
                case Theme.OrangeRed:
                    mergedDictionaries.Add(new OrangeRedTheme());
                    statusBarStyleManager.SetOrangeRedTheme();
                    break;
                default:
                    mergedDictionaries.Add(new IndigoBlueTheme());
                    statusBarStyleManager.SetIndigoBlueTheme();
                    break;
            }
        }

        public static void LoadTheme() =>
            ChangeTheme(PreferencesHelper.GetTheme());
    }
}