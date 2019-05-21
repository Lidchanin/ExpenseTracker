using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
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
            get => (ICommand) GetValue(DonutHoleCommandProperty);
            set => SetValue(DonutHoleCommandProperty, value);
        }

        #endregion DonutHoleCommand property

        #region EmptyStateColor property

        public static readonly BindableProperty EmptyStateColorProperty = BindableProperty.Create(
            nameof(EmptyStateColor),
            typeof(SKColor),
            typeof(DonutChartView),
            SKColors.Transparent);

        public SKColor EmptyStateColor
        {
            get => (SKColor) GetValue(EmptyStateColorProperty);
            set => SetValue(EmptyStateColorProperty, value);
        }

        #endregion EmptyStateColor property

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

        #region HolePrimaryText property

        public static readonly BindableProperty HolePrimaryTextProperty = BindableProperty.Create(
            nameof(HolePrimaryText),
            typeof(string),
            typeof(DonutChartView),
            string.Empty);

        public string HolePrimaryText
        {
            get => (string) GetValue(HolePrimaryTextProperty);
            set => SetValue(HolePrimaryTextProperty, value);
        }

        #endregion HolePrimaryText property

        #region HoleSecondaryText property

        public static readonly BindableProperty HoleSecondaryTextProperty = BindableProperty.Create(
            nameof(HoleSecondaryText),
            typeof(string),
            typeof(DonutChartView),
            string.Empty);

        public string HoleSecondaryText
        {
            get => (string) GetValue(HoleSecondaryTextProperty);
            set => SetValue(HoleSecondaryTextProperty, value);
        }

        #endregion HoleSecondaryText property

        #region HolePrimaryTextScale property 

        public static readonly BindableProperty HolePrimaryTextScaleProperty = BindableProperty.Create(
            nameof(HolePrimaryTextScale),
            typeof(float),
            typeof(DonutChartView),
            1f);

        public float HolePrimaryTextScale
        {
            get => (float) GetValue(HolePrimaryTextScaleProperty);
            set => SetValue(HolePrimaryTextScaleProperty, value);
        }

        #endregion HolePrimaryTextScale property

        #region HoleSecondaryTextScale property 

        public static readonly BindableProperty HoleSecondaryTextScaleProperty = BindableProperty.Create(
            nameof(HoleSecondaryTextScale),
            typeof(float),
            typeof(DonutChartView),
            1f);

        public float HoleSecondaryTextScale
        {
            get => (float) GetValue(HoleSecondaryTextScaleProperty);
            set => SetValue(HoleSecondaryTextScaleProperty, value);
        }

        #endregion HoleSecondaryTextScale property

        #region HoleColor property

        public static readonly BindableProperty HoleColorProperty = BindableProperty.Create(
            nameof(HoleColor),
            typeof(SKColor),
            typeof(DonutChartView),
            SKColors.Transparent);

        public SKColor HoleColor
        {
            get => (SKColor) GetValue(HoleColorProperty);
            set => SetValue(HoleColorProperty, value);
        }

        #endregion HoleColor property

        #region HolePrimaryTextColor property

        public static readonly BindableProperty HolePrimaryTextColorProperty = BindableProperty.Create(
            nameof(HolePrimaryTextColor),
            typeof(SKColor),
            typeof(DonutChartView),
            SKColors.Black);

        public SKColor HolePrimaryTextColor
        {
            get => (SKColor) GetValue(HolePrimaryTextColorProperty);
            set => SetValue(HolePrimaryTextColorProperty, value);
        }

        #endregion HolePrimaryTextColor property

        #region HoleSecondaryTextColor property

        public static readonly BindableProperty HoleSecondaryTextColorProperty = BindableProperty.Create(
            nameof(HoleSecondaryTextColor),
            typeof(SKColor),
            typeof(DonutChartView),
            SKColors.Black);

        public SKColor HoleSecondaryTextColor
        {
            get => (SKColor) GetValue(HoleSecondaryTextColorProperty);
            set => SetValue(HoleSecondaryTextColorProperty, value);
        }

        #endregion HoleSecondaryTextColor property

        #region SeparatorsWidth property 

        public static readonly BindableProperty SeparatorsWidthProperty = BindableProperty.Create(
            nameof(SeparatorsWidth),
            typeof(float),
            typeof(DonutChartView),
            2f);

        public float SeparatorsWidth
        {
            get => (float) GetValue(SeparatorsWidthProperty);
            set => SetValue(SeparatorsWidthProperty, value);
        }

        #endregion SeparatorsWidth property

        #region SeparatorsColor property

        public static readonly BindableProperty SeparatorsColorProperty = BindableProperty.Create(
            nameof(SeparatorsColor),
            typeof(SKColor),
            typeof(DonutChartView),
            SKColors.Black);

        public SKColor SeparatorsColor
        {
            get => (SKColor) GetValue(SeparatorsColorProperty);
            set => SetValue(SeparatorsColorProperty, value);
        }

        #endregion SeparatorsColor property

        #endregion Bindable Properties

        private readonly List<long> _touchIds = new List<long>();

        public DonutChartView()
        {
            PaintSurface += OnPaintCanvas;

            EnableTouchEvents = true;
            Touch += DonutChartView_Touch;
        }

        #region Private methods

        //todo [!!!] ItemSource changed and elements changed
        private static void OnChartChanged(BindableObject bindable) =>
            ((SKCanvasView) bindable).InvalidateSurface();

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e) =>
            DrawContent(e.Surface.Canvas, e.Info.Width, e.Info.Height);

        private void DonutChartView_Touch(object sender, SKTouchEventArgs e)
        {
            if (sender == null)
                return;

            e.Handled = true;

            var touchLocation = e.Location;

            switch (e.ActionType)
            {
                case SKTouchAction.Pressed:
                    if (DonutChartHelper.HitTest(touchLocation, CanvasSize.Width, CanvasSize.Height))
                        _touchIds.Add(e.Id);
                    break;
                case SKTouchAction.Released:
                    if (_touchIds.Contains(e.Id)) _touchIds.Remove(e.Id);
                    OnTouched(touchLocation);
                    break;
                case SKTouchAction.Cancelled:
                case SKTouchAction.Exited:
                    if (_touchIds.Contains(e.Id)) _touchIds.Remove(e.Id);
                    break;
            }
        }

        //todo [?] command parameter
        private void OnTouched(SKPoint touchLocation)
        {
            var translatedLocation = new SKPoint(
                touchLocation.X - CanvasSize.Width / 2,
                touchLocation.Y - CanvasSize.Height / 2);

            if (DonutChartHelper.HolePath.Contains(translatedLocation.X, translatedLocation.Y))
            {
                DonutHoleCommand.Execute(null);
                return;
            }

            for (var i = 0; i < DonutChartHelper.SectorsPaths.Count; i++)
            {
                if (DonutChartHelper.SectorsPaths[i].Contains(translatedLocation.X, translatedLocation.Y))
                {
                    DonutSectorCommand.Execute(i);
                    return;
                }
            }
        }

        private void DrawContent(SKCanvas canvas, int width, int height)
        {
            //todo [?] Do smth with InnerMargin
            const float innerMargin = 20f;

            using (new SKAutoCanvasRestore(canvas))
            {
                canvas.Translate(width / 2f, height / 2f);

                var outerRadius = (Math.Min(width, height) - 2.0f * innerMargin) / 2.0f;
                var innerRadius = outerRadius * HoleRadius;

                if (ItemSource.Count == 0)
                    DrawEmptyState(canvas, outerRadius, innerRadius);
                else
                    DrawSectors(canvas, outerRadius, innerRadius);
                DrawHole(canvas, innerRadius);
                DrawTextInHole(canvas, innerRadius);
                DrawSeparators(canvas, outerRadius, innerRadius);
            }
        }

        private void DrawEmptyState(SKCanvas canvas, float outerRadius, float innerRadius) =>
            DonutChartHelper.DrawEmptyState(canvas, outerRadius, innerRadius, EmptyStateColor);

        private void DrawSectors(SKCanvas canvas, float outerRadius, float innerRadius) =>
            DonutChartHelper.DrawSectors(canvas, outerRadius, innerRadius, ItemSource);

        private void DrawSeparators(SKCanvas canvas, float outerRadius, float innerRadius) =>
            DonutChartHelper.DrawSeparators(canvas, outerRadius, innerRadius, SeparatorsColor, SeparatorsWidth,
                ItemSource);

        private void DrawHole(SKCanvas canvas, float innerRadius) =>
            DonutChartHelper.DrawHole(canvas, innerRadius, HoleColor);

        private void DrawTextInHole(SKCanvas canvas, float innerRadius) =>
            DonutChartHelper.DrawTextInHole(canvas, innerRadius, HolePrimaryTextScale, HoleSecondaryTextScale,
                HolePrimaryText, HoleSecondaryText, HolePrimaryTextColor, HoleSecondaryTextColor);

        #endregion Private methods
    }
}