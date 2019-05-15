using System.ComponentModel;
using ExpenseTracker.Helpers;

namespace ExpenseTracker.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly IExpenseDatabaseHelper ExpenseDatabaseHelper = ExpensesDatabaseHelper.Instance;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}