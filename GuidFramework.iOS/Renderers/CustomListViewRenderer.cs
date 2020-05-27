using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace GuidFramework.iOS.Renderers
{
	/// <summary>
	/// CustomListViewRenderer
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.iOS.ListViewRenderer" />
	public class CustomListViewRenderer : ListViewRenderer
	{
		/// <summary>
		/// Method that runs when the Element property changes on the ListView
		/// </summary>
		/// <param name="e">The eventarguments</param>
		protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
		{
			base.OnElementChanged(e);

			// Set the insets for the seperator to zero and add an empty footer to eliminate the empty listitems
			if (Control != null)
			{
				Control.SeparatorInset = UIEdgeInsets.Zero;
				Control.SeparatorColor = Color.Transparent.ToUIColor();
				Control.SeparatorStyle = UITableViewCellSeparatorStyle.None;

				if (Control.TableFooterView == null)
					Control.TableFooterView = new UIView();
			}
		}
	}
}