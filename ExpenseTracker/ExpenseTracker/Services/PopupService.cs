using ExpenseTracker.Enums;
using ExpenseTracker.Models;
using ExpenseTracker.Pages.Popups;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;

namespace ExpenseTracker.Services
{
    public class PopupService
    {
        #region Instance

        private static PopupService _instance;

        public static readonly PopupService Instance = _instance ?? (_instance = new PopupService());

        #endregion Instance

        public static async Task<DatePeriod> ShowDatePeriodSelectorPopupAsync(DatePeriod datePeriod)
        {
            return await ShowPopup(new DatePeriodSelectorPopup(datePeriod));
        }

        public static async Task<Tuple<DateTime, DateTime>> ShowDateSelectorPopupAsync()
        {
            return await ShowPopup(new DateSelectorPopup());
        }

        public static async Task<Expense> ShowAddExpensePopupAsync()
        {
            return await ShowPopup(new AddExpensePopup());
        }

        #region Private methods

        private static async Task<T> ShowPopup<T>(BasePopup<T> popup)
        {
            try
            {
                await PopupNavigation.Instance.PushAsync(popup);
                var result = await popup.PageClosedTask;
                await PopupNavigation.Instance.PopAsync();
                return result;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        #endregion Private methods
    }
}