using System;
using InnovationLabInventory.Droid.Renderers;
using InnovationLabInventory.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(LoginEntry), typeof(LoginEntryRenderer))]

namespace InnovationLabInventory.Droid.Renderers
{
    public class LoginEntryRenderer : EntryRenderer
    {
        public LoginEntryRenderer()
        {
           
        }protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
                Control.Background = null;
            }
        }

    }
}

