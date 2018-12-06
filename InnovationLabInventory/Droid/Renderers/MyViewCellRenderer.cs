using Android.Content;
using Android.Views;
using InnovationLabInventory.Droid.Renderers;
using InnovationLabInventory.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

[assembly: ExportRenderer(typeof(MyViewCell), typeof(MyViewCellRenderer))]
namespace InnovationLabInventory.Droid.Renderers
{
    public class MyViewCellRenderer : ViewCellRenderer
    {
        protected override View GetCellCore(Cell item, View convertView, ViewGroup parent, Context context)
        {
            var cell = base.GetCellCore(item, convertView, parent, context);
            var listView = parent as Android.Widget.ListView;

            if (listView != null)
            {
                listView.SetSelector(Android.Resource.Color.HoloOrangeLight);
                listView.CacheColorHint = Android.Graphics.Color.Orange;
            }

            return cell;
        }
    }
}