using GuidFramework.Controls;
using GuidFramework.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly:ExportRenderer(typeof(CustomViewCell), typeof(CustomViewCellRenderer))]
namespace GuidFramework.iOS.Renderers
{
	/// <summary>
	/// Custom viewcell renderer
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.iOS.ViewCellRenderer" />
	public class CustomViewCellRenderer : ViewCellRenderer
	{
		/// <summary>
		/// Gets the cell.
		/// </summary>
		/// <param name="item">The item.</param>
		/// <param name="reusableCell">The reusable cell.</param>
		/// <param name="tv">The tv.</param>
		/// <returns></returns>
		public override UITableViewCell GetCell(Cell item, UITableViewCell reusableCell, UITableView tv)
		{
			UIKit.UITableViewCell cell = base.GetCell(item, reusableCell, tv);
			CustomViewCell view = item as CustomViewCell;
			cell.SelectedBackgroundView = new UIView
			{
				BackgroundColor = view.SelectedBackgroundColor.ToUIColor(),
			};
			cell.SelectionStyle = UITableViewCellSelectionStyle.None;
			cell.SeparatorInset = UIEdgeInsets.Zero;
			cell.TintColor = Color.Transparent.ToUIColor();

			return cell;
		}
	}
}