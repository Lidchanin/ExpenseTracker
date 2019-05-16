using SkiaSharp;
using System;

namespace ExpenseTracker.Controls.DonutChart
{
    public static class DonutChartHelper
    {
        private const float UprightAngle = 1.57079637050629f;
        private const float TotalAngle = 6.28318548202515f;

        public static SKPath CreateSectorPath(
            float start,
            float end,
            float outerRadius,
            float innerRadius = 0.0f,
            float margin = 0.0f)
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

            var num1 = (TotalAngle * start - UprightAngle);
            var num2 = TotalAngle * end - UprightAngle;
            var arcSize = num2 - num1 > Math.PI
                ? SKPathArcSize.Large
                : SKPathArcSize.Small;
            var num3 = (num2 - num1) / 2.0f;
            var num4 = (outerRadius - innerRadius) / 2.0f;
            var num5 = outerRadius == 0.0f
                ? 0.0f
                : margin / (TotalAngle * outerRadius) * TotalAngle;
            var num6 = innerRadius == 0.0f
                ? 0.0f
                : margin / (TotalAngle * innerRadius) * TotalAngle;

            var circlePoint1 = GetCirclePoint(outerRadius, num1 + num5);
            var circlePoint2 = GetCirclePoint(outerRadius, num2 - num5);
            var circlePoint3 = GetCirclePoint(innerRadius, num2 - num6);
            var circlePoint4 = GetCirclePoint(innerRadius, num1 + num6);

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

        public static SKPath CreateHolePath(float innerRadius)
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

        public static bool HitTest(SKPoint touchLocation, float canvasWidth, float canvasHeight) =>
            new SKRect(0, 0, canvasWidth, canvasHeight).Contains(touchLocation);

        #region Private methods

        private static SKPoint GetCirclePoint(float radius, float angle) =>
            new SKPoint(radius * (float) Math.Cos(angle), radius * (float) Math.Sin(angle));

        #endregion Private methods
    }
}