using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            typeof(DonutChartView),
            propertyChanged: OnChartChanged);

        public ObservableCollection<DonutChartItem> ItemSource
        {
            get => (ObservableCollection<DonutChartItem>) GetValue(ItemSourceProperty);
            set => SetValue(ItemSourceProperty, value);
        }

        #endregion ItemSource property

        #region SectorCommand property

        public static readonly BindableProperty SectorCommandProperty = BindableProperty.Create(
            nameof(SectorCommand),
            typeof(ICommand),
            typeof(DonutChartView));

        public ICommand SectorCommand
        {
            get => (ICommand) GetValue(SectorCommandProperty);
            set => SetValue(SectorCommandProperty, value);
        }

        #endregion SectorCommand property

        #region HoleCommand property

        public static readonly BindableProperty HoleCommandProperty = BindableProperty.Create(
            nameof(HoleCommand),
            typeof(ICommand),
            typeof(DonutChartView));

        public ICommand HoleCommand
        {
            get => (ICommand) GetValue(HoleCommandProperty);
            set => SetValue(HoleCommandProperty, value);
        }

        #endregion HoleCommand property

        #region EmptyStateColor property

        public static readonly BindableProperty EmptyStateColorProperty = BindableProperty.Create(
            nameof(EmptyStateColor),
            typeof(Color),
            typeof(DonutChartView),
            Color.Transparent,
            propertyChanged: OnChartChanged);

        public Color EmptyStateColor
        {
            get => (Color) GetValue(EmptyStateColorProperty);
            set => SetValue(EmptyStateColorProperty, value);
        }

        #endregion EmptyStateColor property

        #region HoleRadius property 

        //todo [?] 0 <= HoleRadius <= 1
        public static readonly BindableProperty HoleRadiusProperty = BindableProperty.Create(
            nameof(HoleRadius),
            typeof(float),
            typeof(DonutChartView),
            0.5f,
            propertyChanged: OnChartChanged);

        public float HoleRadius
        {
            get => (float) GetValue(HoleRadiusProperty);
            set => SetValue(HoleRadiusProperty, value);
        }

        #endregion HoleRadius property

        #region HolePrimaryText property

        public static readonly BindableProperty HolePrimaryTextProperty = BindableProperty.Create(
            nameof(HolePrimaryText),
            typeof(string),
            typeof(DonutChartView),
            string.Empty,
            propertyChanged: OnChartChanged);

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
            string.Empty,
            propertyChanged: OnChartChanged);

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
            1f,
            propertyChanged: OnChartChanged);

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
            1f,
            propertyChanged: OnChartChanged);

        public float HoleSecondaryTextScale
        {
            get => (float) GetValue(HoleSecondaryTextScaleProperty);
            set => SetValue(HoleSecondaryTextScaleProperty, value);
        }

        #endregion HoleSecondaryTextScale property

        #region HoleColor property

        public static readonly BindableProperty HoleColorProperty = BindableProperty.Create(
            nameof(HoleColor),
            typeof(Color),
            typeof(DonutChartView),
            Color.Transparent,
            propertyChanged: OnChartChanged);

        public Color HoleColor
        {
            get => (Color) GetValue(HoleColorProperty);
            set => SetValue(HoleColorProperty, value);
        }

        #endregion HoleColor property

        #region HolePrimaryTextColor property

        public static readonly BindableProperty HolePrimaryTextColorProperty = BindableProperty.Create(
            nameof(HolePrimaryTextColor),
            typeof(Color),
            typeof(DonutChartView),
            Color.Black,
            propertyChanged: OnChartChanged);

        public Color HolePrimaryTextColor
        {
            get => (Color) GetValue(HolePrimaryTextColorProperty);
            set => SetValue(HolePrimaryTextColorProperty, value);
        }

        #endregion HolePrimaryTextColor property

        #region HoleSecondaryTextColor property

        public static readonly BindableProperty HoleSecondaryTextColorProperty = BindableProperty.Create(
            nameof(HoleSecondaryTextColor),
            typeof(Color),
            typeof(DonutChartView),
            Color.Black,
            propertyChanged: OnChartChanged);

        public Color HoleSecondaryTextColor
        {
            get => (Color) GetValue(HoleSecondaryTextColorProperty);
            set => SetValue(HoleSecondaryTextColorProperty, value);
        }

        #endregion HoleSecondaryTextColor property

        #region InnerMargin property 

        public static readonly BindableProperty InnerMarginProperty = BindableProperty.Create(
            nameof(InnerMargin),
            typeof(float),
            typeof(DonutChartView),
            100f,
            propertyChanged: OnChartChanged);

        public float InnerMargin
        {
            get => (float) GetValue(InnerMarginProperty);
            set => SetValue(InnerMarginProperty, value);
        }

        #endregion InnerMargin property

        #region LineToCircleLength property 

        public static readonly BindableProperty LineToCircleLengthProperty = BindableProperty.Create(
            nameof(LineToCircleLength),
            typeof(float),
            typeof(DonutChartView),
            20f,
            propertyChanged: OnChartChanged);

        public float LineToCircleLength
        {
            get => (float) GetValue(LineToCircleLengthProperty);
            set => SetValue(LineToCircleLengthProperty, value);
        }

        #endregion LineToCircleLength property

        #region DescriptionCircleRadius property 

        public static readonly BindableProperty DescriptionCircleRadiusProperty = BindableProperty.Create(
            nameof(DescriptionCircleRadius),
            typeof(float),
            typeof(DonutChartView),
            30f,
            propertyChanged: OnChartChanged);

        public float DescriptionCircleRadius
        {
            get => (float) GetValue(DescriptionCircleRadiusProperty);
            set => SetValue(DescriptionCircleRadiusProperty, value);
        }

        #endregion DescriptionCircleRadius property

        #region SeparatorsWidth property 

        public static readonly BindableProperty SeparatorsWidthProperty = BindableProperty.Create(
            nameof(SeparatorsWidth),
            typeof(float),
            typeof(DonutChartView),
            2f,
            propertyChanged: OnChartChanged);

        public float SeparatorsWidth
        {
            get => (float) GetValue(SeparatorsWidthProperty);
            set => SetValue(SeparatorsWidthProperty, value);
        }

        #endregion SeparatorsWidth property

        #region SeparatorsColor property

        public static readonly BindableProperty SeparatorsColorProperty = BindableProperty.Create(
            nameof(SeparatorsColor),
            typeof(Color),
            typeof(DonutChartView),
            Color.Black,
            propertyChanged: OnChartChanged);

        public Color SeparatorsColor
        {
            get => (Color) GetValue(SeparatorsColorProperty);
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

        private static void OnChartChanged(BindableObject bindable, object oldValue, object newValue) =>
            ((SKCanvasView) bindable).InvalidateSurface();

        private void OnPaintCanvas(object sender, SKPaintSurfaceEventArgs e)
        {
            DrawContent(e.Surface.Canvas, e.Info.Width, e.Info.Height);

            ItemSource.CollectionChanged += (o, args) =>
            {
                ((SKCanvasView) sender).InvalidateSurface();
                DonutChartHelper.SectorsPaths.Clear();
                DonutChartHelper.DescriptionsPaths.Clear();
            };
        }

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
                HoleCommand.Execute(null);
                return;
            }

            for (var i = 0; i < DonutChartHelper.SectorsPaths.Count; i++)
            {
                if (DonutChartHelper.SectorsPaths[i].Contains(translatedLocation.X, translatedLocation.Y) ||
                    DonutChartHelper.DescriptionsPaths[i].Contains(translatedLocation.X, translatedLocation.Y))
                {
                    SectorCommand.Execute(i);
                    return;
                }
            }
        }

        private void DrawContent(SKCanvas canvas, int width, int height)
        {
            canvas.Clear();
            using (new SKAutoCanvasRestore(canvas))
            {
                canvas.Translate(width / 2f, height / 2f);

                var outerRadius = (Math.Min(width, height) - 2.0f * InnerMargin) / 2.0f;
                var innerRadius = outerRadius * HoleRadius;

                if (ItemSource.Count == 0)
                    DrawEmptyState(canvas, outerRadius, innerRadius);
                else
                    DrawSectors(canvas, outerRadius, innerRadius);
                DrawHole(canvas, innerRadius);
                DrawTextInHole(canvas, innerRadius);
                DrawSeparators(canvas, outerRadius, innerRadius);

                DrawDescriptions(canvas, outerRadius);
            }
        }

        private void DrawEmptyState(SKCanvas canvas, float outerRadius, float innerRadius) =>
            DonutChartHelper.DrawEmptyState(canvas, outerRadius, innerRadius, EmptyStateColor.ToSKColor());

        private void DrawSectors(SKCanvas canvas, float outerRadius, float innerRadius) =>
            DonutChartHelper.DrawSectors(canvas, outerRadius, innerRadius, ItemSource);

        private void DrawSeparators(SKCanvas canvas, float outerRadius, float innerRadius) =>
            DonutChartHelper.DrawSeparators(canvas, outerRadius, innerRadius, SeparatorsColor.ToSKColor(),
                SeparatorsWidth, ItemSource);

        private void DrawHole(SKCanvas canvas, float innerRadius) =>
            DonutChartHelper.DrawHole(canvas, innerRadius, HoleColor.ToSKColor());

        private void DrawTextInHole(SKCanvas canvas, float innerRadius) =>
            DonutChartHelper.DrawTextInHole(canvas, innerRadius, HolePrimaryTextScale, HoleSecondaryTextScale,
                HolePrimaryText, HoleSecondaryText, HolePrimaryTextColor.ToSKColor(),
                HoleSecondaryTextColor.ToSKColor());

        private void DrawDescriptions(SKCanvas canvas, float outerRadius) =>
            DonutChartHelper.DrawDescriptions(canvas, outerRadius, SeparatorsColor.ToSKColor(), SeparatorsWidth,
                ItemSource, DescriptionCircleRadius, LineToCircleLength);

        #endregion Private methods
    }
}