using ExpenseTracker.Data;
using System.ComponentModel;

namespace ExpenseTracker.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        protected readonly IExpenseRepository ExpenseRepository = ExpensesRepository.Instance;

        public event PropertyChangedEventHandler PropertyChanged;
    }
}