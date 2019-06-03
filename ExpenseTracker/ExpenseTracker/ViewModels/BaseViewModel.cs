using ExpenseTracker.Data;
using System.ComponentModel;

namespace ExpenseTracker.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected readonly IExpenseRepository ExpenseRepository = ExpensesRepository.Instance;
    }
}