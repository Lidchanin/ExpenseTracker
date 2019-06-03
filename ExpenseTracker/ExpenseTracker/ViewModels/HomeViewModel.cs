using ExpenseTracker.Controls.DonutChart;
using ExpenseTracker.Data.DTOs;
using ExpenseTracker.Helpers;
using ExpenseTracker.Services;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseTracker.ViewModels
{
    public class HomeViewModel : BaseViewModel
    {
        public string HoleText { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public ObservableCollection<CategoryWithCostSum> CategoriesWithCostSum { get; set; }

        public ObservableCollection<DonutChartItem> ChartItems { get; set; } =
            new ObservableCollection<DonutChartItem>();

        public ICommand SectorTouchCommand { get; set; }
        public ICommand HoleTouchCommand { get; set; }

        public HomeViewModel()
        {
            SectorTouchCommand = new Command(SectorTouchExecute);
            HoleTouchCommand = new Command(HoleTouchExecute);

            InitData();
        }

        #region Private methods

        private async void InitData()
        {
            CategoriesWithCostSum = new ObservableCollection<CategoryWithCostSum>(
                await ExpenseRepository.GetCategoriesWithCostSumAsync());

            var skBitmapHelper = DependencyService.Get<ISKBitmapService>();

            foreach (var item in CategoriesWithCostSum)
            {
                var bitmap = skBitmapHelper.GetSKBitmap(item.File);

                ChartItems.Add(new DonutChartItem((float) item.TotalSum, item.HexColor, bitmap));
            }

            HoleText = CategoriesWithCostSum.Sum(x => x.TotalSum).ToString(CultureInfo.CurrentCulture);
        }

        private void SectorTouchExecute(object commandParameter)
        {
            Debug.WriteLine($"============ Clicked on {commandParameter} sector");
        }

        private void HoleTouchExecute()
        {
            if (ChartItems == null)
                ChartItems = new ObservableCollection<DonutChartItem>();

            Random randomGen = new Random();
            int num1 = randomGen.Next(0, 10);
            int num2 = randomGen.Next(0, 10);
            int num3 = randomGen.Next(0, 10);
            int num4 = randomGen.Next(0, 10);
            int num5 = randomGen.Next(0, 10);
            int num6 = randomGen.Next(0, 10);

            var pngHelper = DependencyService.Get<ISKBitmapService>();
            var bitmap = pngHelper.GetSKBitmap(ConstantHelper.PlaceholderIcon);

            ChartItems.Add(new DonutChartItem(randomGen.Next(100, 1000), $"#{num1}{num2}{num3}{num4}{num5}{num6}",
                bitmap));
        }

        #endregion Private methods
    }
}