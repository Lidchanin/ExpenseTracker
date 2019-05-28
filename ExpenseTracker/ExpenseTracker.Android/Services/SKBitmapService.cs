using Android.Graphics;
using ExpenseTracker.Services;
using SkiaSharp;
using SkiaSharp.Views.Android;
using System;
using Xamarin.Forms;

[assembly: Dependency(typeof(ExpenseTracker.Droid.Services.SKBitmapService))]

namespace ExpenseTracker.Droid.Services
{
    public class SKBitmapService : ISKBitmapService
    {
        public SKBitmap GetSKBitmap(string filename)
        {
            int id;
            try
            {
                id = (int) typeof(Resource.Drawable).GetField(filename.Replace(".png", "")).GetValue(null);
            }
            catch (Exception)
            {
                id = (int) typeof(Resource.Drawable).GetField("ic_placeholder").GetValue(null);
            }

            return BitmapFactory.DecodeResource(Android.App.Application.Context.Resources, id).ToSKBitmap();
        }
    }
}