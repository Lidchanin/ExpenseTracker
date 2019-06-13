using System;
using ExpenseTracker.Enums;
using ExpenseTracker.Helpers;
using ExpenseTracker.Services;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage
    {
        public SettingsPage()
        {
            InitializeComponent();
        }

        private void IndigoButton_OnClicked(object sender, EventArgs e)
        {
            ThemeManager.ChangeTheme(Theme.IndigoBlue);
        }

        private void OrangeButton_OnClicked(object sender, EventArgs e)
        {
            ThemeManager.ChangeTheme(Theme.OrangeRed);
        }

        private void MondayButton_OnClicked(object sender, EventArgs e)
        {
            PreferencesHelper.SetStartOfWeek(DayOfWeek.Monday);
        }

        private void SundayButton_OnClicked(object sender, EventArgs e)
        {
            PreferencesHelper.SetStartOfWeek(DayOfWeek.Sunday);
        }
    }
}