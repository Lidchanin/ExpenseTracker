using ExpenseTracker.Services;
using SkiaSharp;
using SkiaSharp.Views.iOS;
using System;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExpenseTracker.iOS.Services.SKBitmapService))]

namespace ExpenseTracker.iOS.Services
{
    public class SKBitmapService : ISKBitmapService
    {
        public SKBitmap GetSKBitmap(string filename)
        {
            try
            {
                return UIImage.FromBundle(filename).ToSKBitmap();
            }
            catch (Exception)
            {
                return UIImage.FromBundle("ic_placeholder.png").ToSKBitmap();
            }
        }
    }
}