using System;
using Xamarin.Forms;
using InnovationLabInventory.Droid.Renderers;
using InnovationLabInventory.Renderers;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginButton), typeof(LoginButtonRenderer))]
namespace InnovationLabInventory.Droid.Renderers
{
    public class LoginButtonRenderer : ButtonRenderer
    {
        public LoginButtonRenderer()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = null;
            }
        }
    }
}

