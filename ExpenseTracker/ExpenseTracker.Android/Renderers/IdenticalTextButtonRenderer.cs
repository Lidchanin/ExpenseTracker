using Android.Content;
using ExpenseTracker.Controls;
using ExpenseTracker.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using ButtonRenderer = Xamarin.Forms.Platform.Android.FastRenderers.ButtonRenderer;

[assembly: ExportRenderer(typeof(IdenticalTextButton), typeof(IdenticalTextButtonRenderer))]
namespace ExpenseTracker.Droid.Renderers
{
    public class IdenticalTextButtonRenderer : ButtonRenderer
    {
        public IdenticalTextButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            var button = Control;
            button.SetAllCaps(false);
        }
    }
}