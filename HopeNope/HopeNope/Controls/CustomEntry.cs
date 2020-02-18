using Xamarin.Forms;

namespace HopeNope.Controls
{
	public class CustomEntry : Entry
	{
		/// <summary>
		/// Bindable property for UnderlineColorProperty
		/// </summary>
		public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(CustomEntry), Color.Transparent);

		/// <summary>
		/// Gets or sets the UnderlineColor
		/// </summary>
		public Color UnderlineColor
		{
			get { return (Color)GetValue(UnderlineColorProperty); }
			set { SetValue(UnderlineColorProperty, value); }
		}
	}
}
