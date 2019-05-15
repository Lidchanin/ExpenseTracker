using System;

namespace ExpenseTracker.Extensions
{
    public static class DateTimeExtension
    {
        public static DateTime ToBeginningOfMonth(this DateTime dateTime) =>
            new DateTime(dateTime.Year, dateTime.Month, 1);

        public static DateTime ToEndingOfMonth(this DateTime dateTime) =>
            dateTime.ToBeginningOfMonth().AddMonths(1).AddMilliseconds(-1);
    }
}