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

            var tempChartElements = new List<DonutChartItem>
            {
                new DonutChartItem(300)
                {
                    PrimaryText = "0",
                    SecondaryText = "100",
                    SectionColor = SKColors.DarkBlue,
                },
                new DonutChartItem(300)
                {
                    PrimaryText = "1",
                    SecondaryText = "100",
                    SectionColor = SKColors.Red
                },
                new DonutChartItem(200)
                {
                    PrimaryText = "2",
                    SecondaryText = "200",
                    SectionColor = SKColors.Blue
                },
                new DonutChartItem(300)
                {
                    PrimaryText = "3",
                    SecondaryText = "300",
                    SectionColor = SKColors.Yellow
                },
                new DonutChartItem(400)
                {
                    PrimaryText = "4",
                    SecondaryText = "400",
                    SectionColor = SKColors.Green
                },
                new DonutChartItem(400)
                {
                    PrimaryText = "5",
                    SecondaryText = "400",
                    SectionColor = SKColors.Gray
                },
                new DonutChartItem(300)
                {
                    PrimaryText = "6",
                    SecondaryText = "100",
                    SectionColor = SKColors.Red
                },
                new DonutChartItem(200)
                {
                    PrimaryText = "7",
                    SecondaryText = "200",
                    SectionColor = SKColors.Blue
                },
                new DonutChartItem(300)
                {
                    PrimaryText = "8",
                    SecondaryText = "300",
                    SectionColor = SKColors.Yellow
                },
                new DonutChartItem(400)
                {
                    PrimaryText = "9",
                    SecondaryText = "400",
                    SectionColor = SKColors.Green
                },
                new DonutChartItem(400)
                {
                    PrimaryText = "10",
                    SecondaryText = "400",
                    SectionColor = SKColors.Gray
                },
                new DonutChartItem(400)
                {
                    PrimaryText = "11",
                    SecondaryText = "400",
                    SectionColor = SKColors.DarkCyan
                },
                new DonutChartItem(400)
                {
                    PrimaryText = "12",
                    SecondaryText = "400",
                    SectionColor = SKColors.Gray
                },
            };

            DonutChartView.HoleRadius = 0.3f;
            DonutChartView.ItemSource = tempChartElements;

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