using Rg.Plugins.Popup.Pages;
using System.Threading.Tasks;

namespace ExpenseTracker.Pages.Popups
{
    public abstract class BasePopup<T> : PopupPage
    {
        public Task<T> PageClosedTask => PageClosedTaskCompletionSource.Task;

        protected TaskCompletionSource<T> PageClosedTaskCompletionSource { get; } =
            new TaskCompletionSource<T>();
    }
}