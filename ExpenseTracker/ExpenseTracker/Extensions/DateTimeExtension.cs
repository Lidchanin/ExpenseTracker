using System;
using ExpenseTracker.Helpers;

namespace ExpenseTracker.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime ToBeginningOfDay(this DateTime dateTime) =>
            dateTime.Date;

        public static DateTime ToEndingOfDay(this DateTime dateTime) =>
            new DateTime(dateTime.Year, dateTime.Month, dateTime.Day + 1).AddMilliseconds(-1);

        public static DateTime ToBeginningOfWeek(this DateTime dateTime)
        {
            var diff = (7 + (dateTime.DayOfWeek - PreferencesHelper.GetStartOfWeek())) % 7;
            return dateTime.AddDays(-1 * diff).Date;
        }

        public static DateTime ToEndingOfWeek(this DateTime dateTime) =>
            dateTime.ToBeginningOfWeek().AddDays(7).AddMilliseconds(-1);

        public static DateTime ToBeginningOfMonth(this DateTime dateTime) =>
            new DateTime(dateTime.Year, dateTime.Month, 1);

        public static DateTime ToEndingOfMonth(this DateTime dateTime) =>
            dateTime.ToBeginningOfMonth().AddMonths(1).AddMilliseconds(-1);

        public static DateTime ToBeginningOfYear(this DateTime dateTime) =>
            new DateTime(dateTime.Year, 1, 1);

        public static DateTime ToEndingOfYear(this DateTime dateTime) =>
            dateTime.ToBeginningOfMonth().AddYears(1).AddMilliseconds(-1);
    }
}