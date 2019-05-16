using SkiaSharp;

namespace ExpenseTracker.Controls.DonutChart
{
    public static class CanvasExtensions
    {
        public static void DrawCaptionLabels(
          this SKCanvas canvas,
          string label,
          SKColor labelColor,
          string value,
          SKColor valueColor,
          float textSize,
          SKPoint point,
          SKTextAlign horizontalAlignment)
        {
            bool flag1 = !string.IsNullOrEmpty(label);
            bool flag2 = !string.IsNullOrEmpty(value);
            if (!(flag1 | flag2))
                return;
            int num1 = flag1 & flag2 ? 1 : 0;
            float num2 = textSize * 0.6f;
            float num3 = num1 != 0 ? num2 : 0.0f;
            if (flag1)
            {
                using (SKPaint paint = new SKPaint
                {
                    TextSize = textSize,
                    IsAntialias = true,
                    Color = labelColor,
                    IsStroke = false,
                    TextAlign = horizontalAlignment
                })
                {
                    SKRect bounds = new SKRect();
                    string text = label;
                    double num4 = paint.MeasureText(text, ref bounds);
                    float y = point.Y - (float)((bounds.Top + (double)bounds.Bottom) / 2.0) - num3;
                    canvas.DrawText(text, point.X, y, paint);
                }
            }
            if (!flag2)
                return;
            using (SKPaint paint = new SKPaint
            {
                TextSize = textSize,
                IsAntialias = true,
                FakeBoldText = true,
                Color = valueColor,
                IsStroke = false,
                TextAlign = horizontalAlignment
            })
            {
                SKRect bounds = new SKRect();
                string text = value;
                double num4 = paint.MeasureText(text, ref bounds);
                float y = point.Y - (float)((bounds.Top + (double)bounds.Bottom) / 2.0) + num3;
                canvas.DrawText(text, point.X, y, paint);
            }
        }
    }
}