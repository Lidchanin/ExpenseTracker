using System.Collections.Generic;
using ExpenseTracker.Controls.DonutChart;
using SkiaSharp;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage
    {
        public StartPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var tempChartElements = new List<DonutChartElement>
            {
                new DonutChartElement(300)
                {
                    Label = "0",
                    ValueLabel = "100",
                    Color = SKColors.DarkBlue
                },
                new DonutChartElement(300)
                {
                    Label = "1",
                    ValueLabel = "100",
                    Color = SKColors.Red
                },
                new DonutChartElement(200)
                {
                    Label = "2",
                    ValueLabel = "200",
                    Color = SKColors.Blue
                },
                new DonutChartElement(300)
                {
                    Label = "3",
                    ValueLabel = "300",
                    Color = SKColors.Yellow
                },
                new DonutChartElement(400)
                {
                    Label = "4",
                    ValueLabel = "400",
                    Color = SKColors.Green
                },
                new DonutChartElement(400)
                {
                    Label = "5",
                    ValueLabel = "400",
                    Color = SKColors.Gray
                },
                new DonutChartElement(300)
                {
                    Label = "6",
                    ValueLabel = "100",
                    Color = SKColors.Red
                },
                new DonutChartElement(200)
                {
                    Label = "7",
                    ValueLabel = "200",
                    Color = SKColors.Blue
                },
                new DonutChartElement(300)
                {
                    Label = "8",
                    ValueLabel = "300",
                    Color = SKColors.Yellow
                },
                new DonutChartElement(400)
                {
                    Label = "9",
                    ValueLabel = "400",
                    Color = SKColors.Green
                },
                new DonutChartElement(400)
                {
                    Label = "10",
                    ValueLabel = "400",
                    Color = SKColors.Gray
                },
                new DonutChartElement(400)
                {
                    Label = "11",
                    ValueLabel = "400",
                    Color = SKColors.DarkCyan
                },
                new DonutChartElement(400)
                {
                    Label = "12",
                    ValueLabel = "400",
                    Color = SKColors.Gray
                },
            };

            DonutChartView.DonutChart = new DonutChart
            {
                ChartElements = tempChartElements,
                LabelTextSize = 50f,
                HoleRadius = 0.3f,
            };

            DonutChartView.DonutSectorCommand = new Command(SectorTouchExecute);
            DonutChartView.DonutHoleCommand = new Command(HoleTouchExecute);
        }

        private async void SectorTouchExecute(object commandParameter)
        {
            await DisplayAlert("Title", $"Clicked on \"{commandParameter}\" sector", "OK");
        }

        private async void HoleTouchExecute()
        {
            await DisplayAlert("Title", $"Clicked on \"hole\"", "OK");
        }
    }
}