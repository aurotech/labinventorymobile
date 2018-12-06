using InnovationLabInventory.iOS.Renderers;
using InnovationLabInventory.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
[assembly: ExportRenderer(typeof(MyViewCell), typeof(MyViewCellRenderer))]
namespace InnovationLabInventory.iOS.Renderers
{
    public class MyViewCellRenderer : ViewCellRenderer
    {
        public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
        {
            var cell = base.GetCell(item, reusableCell, tv);
            cell.SelectedBackgroundView = new UIView
            {
                BackgroundColor = Color.FromHex("#e66a33").ToUIColor()
            };
            return cell;
        }
    }
}