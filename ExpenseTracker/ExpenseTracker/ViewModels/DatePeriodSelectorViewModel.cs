using ExpenseTracker.Enums;
using System;
using System.Collections.Generic;

namespace ExpenseTracker.ViewModels
{
    public class DatePeriodSelectorViewModel : BaseViewModel
    {
        public List<DatePeriod> DatePeriods { get; set; }

        public DatePeriodSelectorViewModel()
        {
            DatePeriods = new List<DatePeriod>();

            InitData();
        }

        #region Private methods

        private void InitData()
        {
            foreach (DatePeriod datePeriod in Enum.GetValues(typeof(DatePeriod)))
            {
                if (datePeriod != DatePeriod.None)
                    DatePeriods.Add(datePeriod);
            }
        }

        #endregion Private methods
    }
}