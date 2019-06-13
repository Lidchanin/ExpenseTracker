using System;
using ExpenseTracker.Enums;
using Xamarin.Essentials;

namespace ExpenseTracker.Helpers
{
    public static class PreferencesHelper
    {
        #region DatePeriod

        private const string DatePeriodKey = "DatePeriod";

        public static DatePeriod GetDatePeriod() =>
            (DatePeriod) Preferences.Get(DatePeriodKey, (int) DatePeriod.Weekly);

        public static void SetDatePeriod(DatePeriod datePeriod) =>
            Preferences.Set(DatePeriodKey, (int) datePeriod);

        #endregion DatePeriod

        #region Theme

        private const string SelectedThemeKey = "SelectedTheme";

        public static Theme GetTheme() =>
            (Theme) Preferences.Get(SelectedThemeKey, (int) Theme.IndigoBlue);

        public static void SetTheme(Theme theme) =>
            Preferences.Set(SelectedThemeKey, (int) theme);

        #endregion Theme

        #region StartOfWeek

        private const string StartOfWeekKey = "StartOfWeek";

        public static DayOfWeek GetStartOfWeek() =>
            (DayOfWeek) Preferences.Get(StartOfWeekKey, (int) DayOfWeek.Monday);

        public static void SetStartOfWeek(DayOfWeek startOfWeek) =>
            Preferences.Set(StartOfWeekKey, (int) startOfWeek);

        #endregion StartOfWeek
    }
}