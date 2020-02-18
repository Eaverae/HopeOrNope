using HopeNope.Enums;
using Xamarin.Forms;

namespace HopeNope.Controls
{
	/// <summary>
	/// AdBanner control
	/// </summary>
	/// <seealso cref="Xamarin.Forms.View" />
	public class AdBanner : View
	{
		/// <summary>
		/// The size property
		/// </summary>
		public static readonly BindableProperty SizeProperty = BindableProperty.Create(nameof(Size), typeof(AdBannerSizes), typeof(AdBanner), AdBannerSizes.Standardbanner);

		/// <summary>
		/// The ad identifier property
		/// </summary>
		public static readonly BindableProperty AdIdProperty = BindableProperty.Create(nameof(AdId), typeof(string), typeof(AdBanner), string.Empty);

		/// <summary>
		/// Gets or sets the ad identifier.
		/// </summary>
		/// <value>
		/// The ad identifier.
		/// </value>
		public string AdId
		{
			get { return (string)GetValue(AdIdProperty); }
			set { SetValue(AdIdProperty, value); }
		}

		/// <summary>
		/// Gets or sets the size.
		/// </summary>
		/// <value>
		/// The size.
		/// </value>
		public AdBannerSizes Size
		{
			get { return (AdBannerSizes)GetValue(SizeProperty); }
			set { SetValue(SizeProperty, value); }
		}
	}
}
