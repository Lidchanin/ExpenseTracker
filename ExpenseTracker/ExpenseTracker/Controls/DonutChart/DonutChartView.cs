using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseTracker.Controls.DonutChart
{
    public class DonutChartView : SKCanvasView
    {
        #region Bindable Properties

        #region DonutChart property

        public static readonly BindableProperty DonutChartProperty = BindableProperty.Create(
            nameof(DonutChart),
            typeof(DonutChart),
            typeof(DonutChartView),
            null,
            BindingMode.OneWay,
            null,
            OnChartChanged);

        public DonutChart DonutChart
        {
            get => (DonutChart) GetValue(DonutChartProperty);
            set => SetValue(DonutChartProperty, value);
        }

        #endregion DonutChart property

        #region DonutSectorCommand property

        public static readonly BindableProperty DonutSectorCommandProperty = BindableProperty.Create(
            nameof(DonutSectorCommand),
            typeof(ICommand),
            typeof(DonutChartView));

        public ICommand DonutSectorCommand
        {
            get => (ICommand) GetValue(DonutSectorCommandProperty);
            set => SetValue(DonutSectorCommandProperty, value);
        }

        #endregion DonutHoleCommand property

        #region DonutSectorCommand property

        public static readonly BindableProperty DonutHoleCommandProperty = BindableProperty.Create(
            nameof(DonutHoleCommand),
            typeof(ICommand),
            typeof(DonutChartView));

        public ICommand DonutHoleCommand
        {
            get => (ICommand)GetValue(DonutHoleCommandProperty);
            set => SetValue(DonutHoleCommandProperty, value);
        }

        #endregion DonutHoleCommand property

        #region HoleBackgroundColor property

        public static readonly BindableProperty HoleBackgroundColorProperty = BindableProperty.Create(
            nameof(HoleBackgroundColor),
            typeof(SKColor),
            typeof(DonutChartView),
            SKColors.Transparent);

        public SKColor HoleBackgroundColor
        {
            get => (SKColor) GetValue(HoleBackgroundColorProperty);
            set => SetValue(HoleBackgroundColorProperty, value);
        }

        #endregion HoleBackgroundColor property

        #endregion Bindable Properties

        public DonutChartView()
        {
            PaintSurface += OnPaintCanvas;

            EnableTouchEvents = true;
            Touch += DonutChartView_Touch;
        }

        private static void OnChartChanged(BindableObject bindable, object oldValue, object newValue) =>
            ((SKCanvasView) bindable).InvalidateSurface();

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e) =>
            DonutChart?.Draw(e.Surface.Canvas, e.Info.Width, e.Info.Height);

        private readonly List<long> _touchIds = new List<long>();

        private void DonutChartView_Touch(object sender, SKTouchEventArgs e)
        {
            if(sender == null)
                return;

            e.Handled = true;

            SKPoint touchLocation = e.Location;

            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    if (HitTest(touchLocation))
                    {
                        _touchIds.Add(e.Id);
                    }
                    break;
                case SKTouchAction.Released:
                    if (_touchIds.Contains(e.Id))
                    {
                        _touchIds.Remove(e.Id);
                    }
                    OnTouched(touchLocation);
                    break;
                case SKTouchAction.Cancelled:
                case SKTouchAction.Exited:
                    if (_touchIds.Contains(e.Id))
                    {
                        _touchIds.Remove(e.Id);
                    }
                    break;
            }
        }

        //todo [?] command parameter
        private void OnTouched(SKPoint touchLocation)
        {
            var translatedLocation = new SKPoint(
                touchLocation.X - CanvasSize.Width / 2,
                touchLocation.Y - CanvasSize.Height / 2);

            if (DonutChart.HolePath.Contains(translatedLocation.X, translatedLocation.Y))
            {
                DonutHoleCommand.Execute(null);
                return;
            }
            else
            {
                for (var i = 0; i < DonutChart.SectorsPaths.Count; i++)
                {
                    if (DonutChart.SectorsPaths[i].Contains(translatedLocation.X, translatedLocation.Y))
                    {
                        DonutSectorCommand.Execute(i);
                        return;
                    }
                }
            }
        }

        #region Private methods

        private bool HitTest(SKPoint touchLocation) =>
            new SKRect(0, 0, CanvasSize.Width, CanvasSize.Height).Contains(touchLocation);

        #endregion Private methods
    }
}