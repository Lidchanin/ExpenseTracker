using ExpenseTracker.ViewModels;

namespace ExpenseTracker.Helpers
{
    public static class ViewModelLocator
    {
        public static HomeViewModel HomeViewModel { get; } = new HomeViewModel();
    }
}