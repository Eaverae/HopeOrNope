using Xamarin.Forms;

namespace GuidFramework.Controls
{
	/// <summary>
	/// CustomViewCell for iOS
	/// </summary>
	/// <seealso cref="Xamarin.Forms.ViewCell" />
	public class CustomViewCell : ViewCell
	{
		/// <summary>
		/// The selected background color property
		/// </summary>
		public static readonly BindableProperty SelectedBackgroundColorProperty = BindableProperty.Create(nameof(SelectedBackgroundColor), typeof(Color), typeof(CustomViewCell), Color.Default);

		/// <summary>
		/// Gets or sets the color of the selected background.
		/// </summary>
		/// <value>
		/// The color of the selected background.
		/// </value>
		public Color SelectedBackgroundColor
		{
			get { return (Color)GetValue(SelectedBackgroundColorProperty); }
			set { SetValue(SelectedBackgroundColorProperty, value); }
		}
	}
}
