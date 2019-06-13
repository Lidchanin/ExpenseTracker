using ExpenseTracker.Data;
using ExpenseTracker.Services;
using System.ComponentModel;

namespace ExpenseTracker.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected readonly IExpenseRepository ExpenseRepository = ExpensesRepository.Instance;
        protected readonly PopupService PopupService = PopupService.Instance;
    }
}