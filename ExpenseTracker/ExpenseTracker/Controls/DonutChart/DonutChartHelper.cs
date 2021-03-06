﻿using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ExpenseTracker.Controls.DonutChart
{
    internal static class DonutChartHelper
    {
        internal static readonly List<SKPath> SectorsPaths = new List<SKPath>();
        internal static readonly List<SKPath> DescriptionsPaths = new List<SKPath>();
        internal static SKPath HolePath;

        private const float UprightAngle = 1.57079637050629f;
        private const float TotalAngle = 6.28318548202515f;

        internal static void DrawHole(SKCanvas canvas, float innerRadius, SKColor holeColor)
        {
            HolePath = CreateHolePath(innerRadius);

            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = holeColor,
                IsAntialias = true
            })
            {
                canvas.DrawPath(HolePath, paint);
            }
        }

        internal static void DrawEmptyState(SKCanvas canvas, float outerRadius, float innerRadius,
            SKColor emptyStateColor)
        {
            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                Color = emptyStateColor,
                IsAntialias = true
            })
            {
                var emptyStatePath = CreateEmptyStatePath(outerRadius, innerRadius);
                canvas.DrawPath(emptyStatePath, paint);
            }
        }

        internal static void DrawSeparators(SKCanvas canvas, float outerRadius, float innerRadius,
            SKColor separatorsColor, float separatorsWidth, IReadOnlyList<DonutChartItem> itemSource)
        {
            if (separatorsWidth <= 0 || separatorsColor == SKColors.Transparent)
                return;

            using (var paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = separatorsWidth,
                Color = separatorsColor,
                IsAntialias = true
            })
            {
                var radiusSeparatorsPath = CreateRadiusSeparatorsPath(outerRadius, innerRadius);
                canvas.DrawPath(radiusSeparatorsPath, paint);

                var sumValues = itemSource.Sum(x => Math.Abs(x.Value));
                var start = 0.0f;

                for (var index = 0; index < itemSource.Count; ++index)
                {
                    var chartItem = itemSource.ElementAt(index);
                    var end = start + Math.Abs(chartItem.Value) / sumValues;

                    var sectorSeparatorPath = CreateSectorSeparatorPath(start, end, outerRadius, innerRadius);

                    canvas.DrawPath(sectorSeparatorPath, paint);

                    start = end;
                }
            }
        }

        internal static void DrawSectors(SKCanvas canvas, float outerRadius, float innerRadius,
            IReadOnlyList<DonutChartItem> itemSource)
        {
            var sumValues = itemSource.Sum(x => Math.Abs(x.Value));
            var start = 0.0f;

            for (var index = 0; index < itemSource.Count; ++index)
            {
                var chartItem = itemSource.ElementAt(index);
                var end = start + Math.Abs(chartItem.Value) / sumValues;

                var sectorPath = CreateSectorPath(start, end, outerRadius, innerRadius);

                using (var paint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = SKColor.Parse(chartItem.SectionHexColor),
                    IsAntialias = true
                })
                {
                    canvas.DrawPath(sectorPath, paint);
                }

                start = end;

                SectorsPaths.Add(sectorPath);
            }
        }

        internal static void DrawDescriptions(SKCanvas canvas, float outerRadius, SKColor separatorsColor,
            float separatorsWidth, IReadOnlyList<DonutChartItem> itemSource, float circleRadius,
            float lineToCircleLength)
        {
            var sumValues = itemSource.Sum(x => Math.Abs(x.Value));
            var resizedBitmapSide = (int) GetInnerRectSideOfCircle(circleRadius);
            var separatorPaint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                StrokeWidth = separatorsWidth,
                Color = separatorsColor,
                IsAntialias = true
            };
            var descPaint = new SKPaint
            {
                Style = SKPaintStyle.Fill,
                IsAntialias = true
            };

            var start = 0.0f;

            for (var index = 0; index < itemSource.Count; ++index)
            {
                var chartItem = itemSource.ElementAt(index);

                var end = start + Math.Abs(chartItem.Value) / sumValues;

                if (chartItem.Bitmap != null &&
                    !chartItem.Bitmap.IsEmpty &&
                    end != start)
                {
                    var angle1 = TotalAngle * start - UprightAngle;
                    var angle2 = TotalAngle * end - UprightAngle;
                    var angle = (angle1 + angle2) / 2;

                    var circlePoint1 = GetCirclePoint(outerRadius, angle);
                    var circlePoint2 = GetCirclePoint(outerRadius + lineToCircleLength, angle);
                    var circlePoint3 = GetCirclePoint(outerRadius + lineToCircleLength + circleRadius, angle);

                    var descriptionSeparatorPath =
                        CreateDescriptionSeparatorPath(circleRadius, circlePoint1, circlePoint2, circlePoint3);

                    canvas.DrawPath(descriptionSeparatorPath, separatorPaint);

                    descPaint.Color = SKColor.Parse(chartItem.SectionHexColor);
                    canvas.DrawCircle(circlePoint3.X, circlePoint3.Y, circleRadius - separatorsWidth / 2,
                        descPaint);

                    var resizedBitmap = chartItem.Bitmap.Resize(
                        new SKImageInfo(resizedBitmapSide, resizedBitmapSide),
                        SKFilterQuality.High);

                    canvas.DrawBitmap(resizedBitmap,
                        circlePoint3.X - resizedBitmap.Width / 2f,
                        circlePoint3.Y - resizedBitmap.Height / 2f);

                    start = end;

                    DescriptionsPaths.Add(descriptionSeparatorPath);
                }
            }

            descPaint.Dispose();
            separatorPaint.Dispose();
        }

        internal static void DrawTextInHole(SKCanvas canvas, float innerRadius, float holePrimaryTextScale,
            float holeSecondaryTextScale, string holePrimaryText, string holeSecondaryText,
            SKColor holePrimaryTextColor, SKColor holeSecondaryTextColor)
        {
            if (innerRadius < 0.0f)
                return;

            if (string.IsNullOrEmpty(holePrimaryText) &&
                string.IsNullOrEmpty(holeSecondaryText))
                return;

            var squareSide = (float) GetInnerRectSideOfCircle(innerRadius);

            if (string.IsNullOrEmpty(holeSecondaryText))
            {
                var textPaint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = holePrimaryTextColor,
                    IsAntialias = true
                };

                var startTextWidth = textPaint.MeasureText(holePrimaryText);
                textPaint.TextSize = GetTextSize(holePrimaryTextScale, squareSide, startTextWidth);

                var textHeight = textPaint.FontMetrics.CapHeight;
                var textWidth = textPaint.MeasureText(holePrimaryText);

                while (textHeight > textWidth &&
                       textHeight > squareSide)
                {
                    ReduceTextSize(ref holePrimaryTextScale, ref textPaint, ref textHeight, ref textWidth, squareSide,
                        startTextWidth, holePrimaryText);
                }

                canvas.DrawText(holePrimaryText, -textWidth / 2.0f, textHeight / 2.0f, textPaint);

                textPaint.Dispose();
            }
            else
            {
                var prTextPaint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = holePrimaryTextColor
                };

                var startPrTextWidth = prTextPaint.MeasureText(holePrimaryText);
                prTextPaint.TextSize = GetTextSize(holePrimaryTextScale, squareSide, startPrTextWidth);

                var prTextHeight = prTextPaint.FontMetrics.CapHeight;
                var prTextWidth = prTextPaint.MeasureText(holePrimaryText);

                var secTextPaint = new SKPaint
                {
                    Style = SKPaintStyle.Fill,
                    Color = holeSecondaryTextColor
                };

                var startSecTextWidth = secTextPaint.MeasureText(holeSecondaryText);
                secTextPaint.TextSize = GetTextSize(holeSecondaryTextScale, squareSide, startSecTextWidth);

                var secTextHeight = secTextPaint.FontMetrics.CapHeight;
                var secTextWidth = secTextPaint.MeasureText(holeSecondaryText);

                var emptySectorHeight = GetEmptySectorHeight(squareSide, prTextHeight, secTextHeight);

                //todo [?] Change algorithm for TextSize where (prTextHeight + secTextHeight > squareSide)
                while (prTextHeight > squareSide ||
                       secTextHeight > squareSide ||
                       prTextHeight + secTextHeight > squareSide ||
                       emptySectorHeight < 30f)
                {
                    if (prTextHeight > secTextHeight)
                    {
                        ReduceTextSize(ref holePrimaryTextScale, ref prTextPaint, ref prTextHeight, ref prTextWidth,
                            squareSide, startPrTextWidth, holePrimaryText);
                    }
                    else if (prTextHeight < secTextHeight)
                    {
                        ReduceTextSize(ref holeSecondaryTextScale, ref secTextPaint, ref secTextHeight,
                            ref secTextWidth, squareSide, startSecTextWidth, holeSecondaryText);
                    }
                    else
                    {
                        ReduceTextSize(ref holePrimaryTextScale, ref prTextPaint, ref prTextHeight, ref prTextWidth,
                            squareSide, startPrTextWidth, holePrimaryText);
                        ReduceTextSize(ref holeSecondaryTextScale, ref secTextPaint, ref secTextHeight,
                            ref secTextWidth, squareSide, startSecTextWidth, holeSecondaryText);
                    }

                    emptySectorHeight = GetEmptySectorHeight(squareSide, prTextHeight, secTextHeight);
                }

                var prTemp = -(squareSide / 2.0f - emptySectorHeight - prTextHeight);
                var secTemp = (squareSide / 2.0f - emptySectorHeight);

                canvas.DrawText(holePrimaryText, -prTextWidth / 2.0f, prTemp, prTextPaint);
                canvas.DrawText(holeSecondaryText, -secTextWidth / 2.0f, secTemp, secTextPaint);

                prTextPaint.Dispose();
                secTextPaint.Dispose();
            }
        }

        internal static bool HitTest(SKPoint touchLocation, float canvasWidth, float canvasHeight) =>
            new SKRect(0, 0, canvasWidth, canvasHeight).Contains(touchLocation);

        #region Private methods

        private static SKPath CreateHolePath(float innerRadius)
        {
            var skPath = new SKPath();

            if (innerRadius > 0.0f)
            {
                skPath.AddCircle(0.0f, 0.0f, innerRadius);
                skPath.FillType = SKPathFillType.EvenOdd;
            }

            skPath.Close();

            return skPath;
        }

        private static SKPath CreateEmptyStatePath(float outerRadius, float innerRadius)
        {
            var skPath = new SKPath();

            skPath.AddCircle(0.0f, 0.0f, outerRadius);
            if (innerRadius > 0)
                skPath.AddCircle(0.0f, 0.0f, innerRadius);

            skPath.FillType = SKPathFillType.EvenOdd;

            skPath.Close();

            return skPath;
        }

        private static SKPath CreateRadiusSeparatorsPath(float outerRadius, float innerRadius)
        {
            var skPath = new SKPath();

            skPath.AddCircle(0.0f, 0.0f, outerRadius);
            if (innerRadius > 0)
                skPath.AddCircle(0.0f, 0.0f, innerRadius);

            skPath.Close();

            return skPath;
        }

        private static SKPath CreateSectorSeparatorPath(float start, float end, float outerRadius, float innerRadius)
        {
            var skPath = new SKPath();

            if (start == end)
                return skPath;

            if (end - start == 1.0)
            {
                skPath.AddCircle(0.0f, 0.0f, outerRadius);
                skPath.AddCircle(0.0f, 0.0f, innerRadius);
                skPath.FillType = SKPathFillType.EvenOdd;
                return skPath;
            }

            var angle = (TotalAngle * start - UprightAngle);

            var circlePoint1 = GetCirclePoint(outerRadius, angle);
            var circlePoint2 = GetCirclePoint(innerRadius, angle);

            skPath.MoveTo(circlePoint1);
            skPath.LineTo(circlePoint2);

            skPath.Close();

            return skPath;
        }

        private static SKPath CreateSectorPath(float start, float end, float outerRadius, float innerRadius)
        {
            var skPath = new SKPath();

            if (start == end)
                return skPath;

            if (end - start == 1.0)
            {
                skPath.AddCircle(0.0f, 0.0f, outerRadius);
                skPath.AddCircle(0.0f, 0.0f, innerRadius);
                skPath.FillType = SKPathFillType.EvenOdd;
                return skPath;
            }

            var angle1 = (TotalAngle * start - UprightAngle);
            var angle2 = TotalAngle * end - UprightAngle;
            var arcSize = angle2 - angle1 > Math.PI
                ? SKPathArcSize.Large
                : SKPathArcSize.Small;

            var circlePoint1 = GetCirclePoint(outerRadius, angle1);
            var circlePoint2 = GetCirclePoint(outerRadius, angle2);
            var circlePoint3 = GetCirclePoint(innerRadius, angle2);
            var circlePoint4 = GetCirclePoint(innerRadius, angle1);

            skPath.MoveTo(circlePoint1);
            skPath.ArcTo(outerRadius, outerRadius, 0.0f, arcSize, SKPathDirection.Clockwise, circlePoint2.X,
                circlePoint2.Y);
            skPath.LineTo(circlePoint3);

            if (innerRadius == 0.0)
                skPath.LineTo(circlePoint4);
            else
                skPath.ArcTo(innerRadius, innerRadius, 0.0f, arcSize, SKPathDirection.CounterClockwise, circlePoint4.X,
                    circlePoint4.Y);

            skPath.Close();

            return skPath;
        }

        private static SKPath CreateDescriptionSeparatorPath(float circleRadius, SKPoint circlePoint1,
            SKPoint circlePoint2, SKPoint circlePoint3)
        {
            var skPath = new SKPath();

            skPath.MoveTo(circlePoint1);
            skPath.LineTo(circlePoint2);
            skPath.AddCircle(circlePoint3.X, circlePoint3.Y, circleRadius);

            skPath.Close();

            return skPath;
        }

        private static SKPoint GetCirclePoint(float radius, float angle) =>
            new SKPoint(radius * (float) Math.Cos(angle), radius * (float) Math.Sin(angle));

        private static double GetInnerRectSideOfCircle(float circleRadius) =>
            Math.Sqrt(2) * circleRadius;

        private static float GetTextSize(float textScale, float textSquareSide, float textWidth) =>
            // 12f is default SKPaint.TextSize
            textScale * textSquareSide * 12f / textWidth;

        private static float GetEmptySectorHeight(float squareSide, float prTextHeight, float secTextHeight) =>
            (squareSide - prTextHeight - secTextHeight) / 3.0f;

        private static void ReduceTextSize(ref float textScale, ref SKPaint skPaint, ref float textHeight,
            ref float textWidth, float squareSide, float startTextWidth, string text)
        {
            textScale -= 0.1f;
            skPaint.TextSize = GetTextSize(textScale, squareSide, startTextWidth);
            textHeight = skPaint.FontMetrics.CapHeight;
            textWidth = skPaint.MeasureText(text);
        }

        #endregion Private methods
    }
}