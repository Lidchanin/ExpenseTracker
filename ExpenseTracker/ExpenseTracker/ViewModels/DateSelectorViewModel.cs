using System;

namespace ExpenseTracker.ViewModels
{
    public class DateSelectorViewModel : BaseViewModel
    {
        private DateTime _startDate;

        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                if (_startDate != value)
                {
                    _startDate = value;
                    IsDatesValid = CheckIsDatesValid();
                }
            }
        }

        private DateTime _endDate;

        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                if (_endDate != value)
                {
                    _endDate = value;
                    IsDatesValid = CheckIsDatesValid();
                }
            }
        }

        public bool IsDatesValid { get; set; }

        private bool CheckIsDatesValid() =>
            StartDate <= EndDate;
    }
}