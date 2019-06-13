using ExpenseTracker.Controls.DonutChart;
using ExpenseTracker.Data.DTOs;
using ExpenseTracker.Enums;
using ExpenseTracker.Extensions;
using ExpenseTracker.Helpers;
using ExpenseTracker.Models;
using ExpenseTracker.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace ExpenseTracker.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public string HoleText { get; set; }

        public ObservableCollection<CategoryWithCostSum> CategoriesWithCostSum { get; set; }
        public ObservableCollection<DonutChartItem> ChartItems { get; set; }

        public ICommand CalendarItemCommand { get; }
        public ICommand SectorTouchCommand { get; }
        public ICommand HoleTouchCommand { get; }

        public ICommand AddCommand { get; }

        private readonly ISKBitmapService _skBitmapService;

        private DateTime _fromDate;
        private DateTime _toDate;

        public HomeViewModel()
        {
            CalendarItemCommand = new Command(CalendarItemExecute);

            SectorTouchCommand = new Command(SectorTouchExecute);
            HoleTouchCommand = new Command(HoleTouchExecute);

            AddCommand = new Command(AddExecute);

            _skBitmapService = DependencyService.Get<ISKBitmapService>();

            InitData(PreferencesHelper.GetDatePeriod());
        }

        #region Private methods

        //todo [!] Remove isValueFromPicker
        private async void InitData(DatePeriod datePeriod, bool isValueFromPicker = true)
        {
            if (datePeriod != DatePeriod.None && datePeriod != DatePeriod.Custom)
            {
                PreferencesHelper.SetDatePeriod(datePeriod);
            }

            switch (datePeriod)
            {
                case DatePeriod.None:
                    return;
                case DatePeriod.Daily:
                    _fromDate = DateTime.Today.ToBeginningOfDay();
                    _toDate = DateTime.Today.ToEndingOfDay();
                    break;
                case DatePeriod.Weekly:
                    _fromDate = DateTime.Today.ToBeginningOfWeek();
                    _toDate = DateTime.Today.ToEndingOfWeek();
                    break;
                case DatePeriod.Monthly:
                    _fromDate = DateTime.Today.ToBeginningOfMonth();
                    _toDate = DateTime.Today.ToEndingOfMonth();
                    break;
                case DatePeriod.Yearly:
                    _fromDate = DateTime.Today.ToBeginningOfYear();
                    _toDate = DateTime.Today.ToEndingOfYear();
                    break;
                case DatePeriod.AllTime:
                    _fromDate = DateTime.MinValue;
                    _toDate = DateTime.MaxValue;
                    break;
                case DatePeriod.Custom:
                    if (!isValueFromPicker)
                        break;

                    var customDates = await PopupService.ShowDateSelectorPopupAsync();

                    if (customDates == null)
                        return;

                    _fromDate = customDates.Item1.ToBeginningOfDay();
                    _toDate = customDates.Item2.ToEndingOfDay();

                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(datePeriod), datePeriod, null);
            }

            CategoriesWithCostSum = new ObservableCollection<CategoryWithCostSum>(
                await ExpenseRepository.GetCategoriesWithCostSumAsync(_fromDate, _toDate));

            ChartItems = new ObservableCollection<DonutChartItem>();

            CategoriesWithCostSum.ForEach(c => ChartItems.Add(new DonutChartItem(
                (float) c.TotalSum, c.HexColor, _skBitmapService.GetSKBitmap(c.File))));

            HoleText = CategoriesWithCostSum.Sum(x => x.TotalSum).ToString(CultureInfo.CurrentCulture);
        }

        private async void CalendarItemExecute()
        {
            InitData(await PopupService.ShowDatePeriodSelectorPopupAsync(PreferencesHelper.GetDatePeriod()));
        }

        private async void AddExecute()
        {
            var expense = await PopupService.ShowAddExpensePopupAsync();

            if (expense == null)
                return;

            expense = await ExpenseRepository.AddExpenseAsync(expense);

            //todo [!] Update algorithm
            if (expense.Timestamp >= _fromDate && expense.Timestamp <= _toDate)
            {
                InitData(DatePeriod.Custom, false);
            }
        }

        private async void SectorTouchExecute(object commandParameter)
        {
            Debug.WriteLine($"__________________________  {commandParameter}");
            //await Shell.Current.GoToAsync(
            //    $"{RoutingHelper.HomeChartCategoryDetails}" +
            //    $"?{RoutingHelper.CategoryId}={commandParameter}");
        }

        private async void HoleTouchExecute()
        {
        }

        #endregion Private methods
    }
}