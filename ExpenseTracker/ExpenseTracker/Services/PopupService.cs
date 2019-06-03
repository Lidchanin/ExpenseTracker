using System.Threading.Tasks;
using ExpenseTracker.Enums;
using ExpenseTracker.Pages.Popups;
using Rg.Plugins.Popup.Services;

namespace ExpenseTracker.Services
{
    public class PopupService
    {
        #region Instance

        private static PopupService _instance;

        public static readonly PopupService Instance = _instance ?? (_instance = new PopupService());

        #endregion Instance

        public async Task<DatePeriods> ShowDatePeriodPopupAsync()
        {
            var popup = new DatePeriodPopup();
            await PopupNavigation.Instance.PushAsync(popup);
            var result = await popup.PageClosedTask;
            await PopupNavigation.Instance.PopAsync();
            return result;
        }
    }
}