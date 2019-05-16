using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Controls.DonutChart
{
    public class DonutChart
    {
        public float Margin { get; set; } = 20f;
        public float LabelTextSize { get; set; } = 16f;
        // todo 0f < HoleRadius < 1f
        public float HoleRadius { get; set; }

        public List<DonutChartElement> ChartElements { get; set; }

        public SKPath HolePath;
        public List<SKPath> SectorsPaths { get; set; } = new List<SKPath>();

        public void Draw(SKCanvas canvas, int width, int height)
        {
            DrawContent(canvas, width, height);
        }

        /*protected void DrawCaptionElements(
            SKCanvas canvas,
            int width,
            int height,
            List<DonutChartElement> chartElements,
            bool isLeft)
        {
            var num1 = 2f * Margin;
            var num2 = height - 2f * num1;

            var num3 = (float) ((num2 - (double) LabelTextSize) /
                                (chartElements.Count <= 1 ? 1.0 : chartElements.Count - 1));
            for (var index = 0; index < chartElements.Count; ++index)
            {
                var chartElement = chartElements.ElementAt(index);
                var y = num1 + index * num3;
                if (chartElements.Count <= 1)
                    y += (float) ((num2 - (double) LabelTextSize) / 2.0);

                var flag1 = !string.IsNullOrEmpty(chartElement.Label);
                var flag2 = !string.IsNullOrEmpty(chartElement.ValueLabel);

                if (flag1 | flag2)
                {
                    var num4 = flag1 & flag2 ? 1 : 0;
                    var num5 = LabelTextSize * 0.6f;
                    var x1 = isLeft ? Margin : width - Margin - LabelTextSize;
                    using (var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = chartElement.Color
                    })
                    {
                        var rect = SKRect.Create(x1, y, LabelTextSize, LabelTextSize);
                        canvas.DrawRect(rect, paint);
                    }

                    var x2 = !isLeft ? x1 - num5 : x1 + (LabelTextSize + num5);
                    canvas.DrawCaptionLabels(chartElement.Label, chartElement.TextColor, chartElement.ValueLabel,
                        chartElement.Color,
                        LabelTextSize, new SKPoint(x2, y + LabelTextSize / 2f),
                        isLeft ? SKTextAlign.Left : SKTextAlign.Right);
                }
            }
        }*/

        private void DrawContent(SKCanvas canvas, int width, int height)
        {
            DrawCaption(canvas, width, height);
            using (new SKAutoCanvasRestore(canvas))
            {
                canvas.Translate(width / 2f, height / 2f);
                var num = ChartElements.Sum(x => Math.Abs(x.Value));
                var outerRadius = (float) ((Math.Min(width, height) - 2.0 * Margin) / 2.0);
                var start = 0.0f;
                for (var index = 0; index < ChartElements.Count(); ++index)
                {
                    var chartElement = ChartElements.ElementAt(index);
                    var end = start + Math.Abs(chartElement.Value) / num;
                    var sectorPath = RadialHelper.CreateSectorPath(start, end, outerRadius, outerRadius * HoleRadius);

                    using (var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = chartElement.Color,
                        IsAntialias = true
                    })
                    {
                        canvas.DrawPath(sectorPath, paint);
                    }

                    start = end;

                    var holePath = RadialHelper.CreateHolePath(outerRadius * HoleRadius);

                    using (var paint = new SKPaint
                    {
                        Style = SKPaintStyle.Fill,
                        Color = SKColors.Black,
                        IsAntialias = true
                    })
                    {
                        canvas.DrawPath(holePath, paint);
                    }

                    SectorsPaths.Add(sectorPath);
                    HolePath = holePath;
                }
            }
        }

        private void DrawCaption(SKCanvas canvas, int width, int height)
        {
            var num1 = ChartElements.Sum(x => Math.Abs(x.Value));

            var leftChartElements = new List<DonutChartElement>();
            var rightChartElements = new List<DonutChartElement>();

            var index = 0;

            for (var num2 = 0.0f; index < ChartElements.Count() && num2 < num1 / 2.0; ++index)
            {
                var chartElement = ChartElements.ElementAt(index);
                leftChartElements.Add(chartElement);
                num2 += Math.Abs(chartElement.Value);
            }

            for (; index < ChartElements.Count(); ++index)
            {
                var chartElement = ChartElements.ElementAt(index);
                rightChartElements.Add(chartElement);
            }

            rightChartElements.Reverse();

            //DrawCaptionElements(canvas, width, height, leftChartElements, false);
            //DrawCaptionElements(canvas, width, height, rightChartElements, true);
        }
    }
}