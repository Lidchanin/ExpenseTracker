using System.ComponentModel;
using ExpenseTracker.Helpers;

namespace ExpenseTracker.Enums
{
    public enum DatePeriod
    {
        None,
        [Description(ConstantHelper.DailyDatePeriod)]
        Daily,
        [Description(ConstantHelper.WeeklyDatePeriod)]
        Weekly,
        [Description(ConstantHelper.MonthlyDatePeriod)]
        Monthly,
        [Description(ConstantHelper.YearlyDatePeriod)]
        Yearly,
        [Description(ConstantHelper.AllTimeDatePeriod)]
        AllTime,
        [Description(ConstantHelper.CustomDatePeriod)]
        Custom
    }
}