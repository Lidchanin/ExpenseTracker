using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseTracker.Controls.DonutChart
{
    public class DonutChartView : SKCanvasView
    {
        #region Bindable Properties

        #region ItemSource property

        public static readonly BindableProperty ItemSourceProperty = BindableProperty.Create(
            nameof(ItemSource),
            typeof(IReadOnlyList<DonutChartItem>),
            typeof(DonutChartView));

        public IReadOnlyList<DonutChartItem> ItemSource
        {
            get => (IReadOnlyList<DonutChartItem>) GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        #endregion ItemSource property

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

        #region HoleRadius property 
        
        //todo [?] 0 <= HoleRadius <= 1
        public static readonly BindableProperty HoleRadiusProperty = BindableProperty.Create(
            nameof(HoleRadius),
            typeof(float),
            typeof(DonutChartView),
            0.5f);

        public float HoleRadius
        {
            get => (float) GetValue(HoleRadiusProperty);
            set => SetValue(HoleRadiusProperty, value);
        }

        #endregion HoleRadiusColor property

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

        private SKPath _holePath;
        private readonly List<SKPath> _sectorsPaths = new List<SKPath>();

        public DonutChartView()
        {
            PaintSurface += OnPaintCanvas;

            EnableTouchEvents = true;
            Touch += DonutChartView_Touch;
        }

        //todo ItemSource changed and elements changed
        private static void OnChartChanged(BindableObject bindable) =>
            ((SKCanvasView) bindable).InvalidateSurface();

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e) =>
            DrawContent(e.Surface.Canvas, e.Info.Width, e.Info.Height);

        private readonly List<long> _touchIds = new List<long>();

        private void DonutChartView_Touch(object sender, SKTouchEventArgs e)
        {
            if(sender == null)
                return;

            e.Handled = true;

            var touchLocation = e.Location;

            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    if (DonutChartHelper.HitTest(touchLocation, CanvasSize.Width, CanvasSize.Height))
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

        #region Private methods
    
        //todo [?] command parameter
        private void OnTouched(SKPoint touchLocation)
        {
            var translatedLocation = new SKPoint(
                touchLocation.X - CanvasSize.Width / 2,
                touchLocation.Y - CanvasSize.Height / 2);

            if (_holePath.Contains(translatedLocation.X, translatedLocation.Y))
            {
                DonutHoleCommand.Execute(null);
                return;
            }

            for (var i = 0; i < _sectorsPaths.Count; i++)
            {
                if (_sectorsPaths[i].Contains(translatedLocation.X, translatedLocation.Y))
                {
                    DonutSectorCommand.Execute(i);
                    return;
                }
            }
        }

        //todo [REMOVE] InnerMargin 
        private readonly float _innerMargin = 20f;

        private void DrawContent(SKCanvas canvas, int width, int height)
        {
            using (new SKAutoCanvasRestore(canvas))
            {
                canvas.Translate(width / 2f, height / 2f);

                var outerRadius = (Math.Min(width, height) - 2.0f * _innerMargin) / 2.0f;
                var innerRadius = outerRadius * HoleRadius;

                DrawSectors(canvas, outerRadius, innerRadius);
                DrawHole(canvas, innerRadius);
            }
        }

        private void DrawSectors(SKCanvas canvas, float outerRadius, float innerRadius)
        {
            var sumValues = ItemSource.Sum(x => Math.Abs(x.Value));
            var start = 0.0f;

            for (var index = 0; index < ItemSource.Count; ++index)
            {
                var chartItem = ItemSource.ElementAt(index);
                var end = start + Math.Abs(chartItem.Value) / sumValues;

                var sectorPath = DonutChartHelper.CreateSectorPath(start, end, outerRadius, innerRadius);

                using (var paint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = chartItem.SectionColor,
                    IsAntialias = true
                })
                {
                    canvas.DrawPath(sectorPath, paint);
                }

                start = end;

                _sectorsPaths.Add(sectorPath);
            }
        }

        private void DrawHole(SKCanvas canvas, float innerRadius)
        {
            var holePath = DonutChartHelper.CreateHolePath(innerRadius);

            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = SKColors.Black,
                IsAntialias = true
            })
            {
                canvas.DrawPath(holePath, paint);
            }

            _holePath = holePath;
        }

        #endregion Private methods
    }
}