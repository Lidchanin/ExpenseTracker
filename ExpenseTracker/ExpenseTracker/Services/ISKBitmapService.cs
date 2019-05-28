using SkiaSharp;

namespace ExpenseTracker.Services
{
    public interface ISKBitmapService
    {
        SKBitmap GetSKBitmap(string filename);
    }
}