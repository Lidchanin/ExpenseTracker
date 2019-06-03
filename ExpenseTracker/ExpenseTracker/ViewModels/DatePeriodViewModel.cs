using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.ViewModels
{
    public class DatePeriodViewModel : BaseViewModel
    {
        public List<DatePeriods> DatePeriods { get; set; }

        public DatePeriodViewModel()
        {
            DatePeriods = new List<DatePeriods>();

            InitData();
        }

        #region Private methods

        private void InitData()
        {
            foreach (DatePeriods datePeriod in Enum.GetValues(typeof(DatePeriods)))
            {
                if (datePeriod != Enums.DatePeriods.None)
                    DatePeriods.Add(datePeriod);
            }
        }

        #endregion Private methods
    }
}