using Xamarin.Forms;

namespace HopeNope.Controls
{
	/// <summary>
	/// CustomPicker
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Picker" />
	public class CustomPicker : Picker
	{
		/// <summary>
	 /// The accept button text property
	 /// </summary>
		public static readonly BindableProperty AcceptButtonTextProperty = BindableProperty.Create(nameof(AcceptButtonText), typeof(string), typeof(CustomPicker), string.Empty);

		/// <summary>
		/// Gets or sets the accept button text.
		/// </summary>
		/// <value>
		/// The accept button text.
		/// </value>
		public string AcceptButtonText
		{
			get { return (string)GetValue(AcceptButtonTextProperty); }
			set { SetValue(AcceptButtonTextProperty, value); }
		}

		/// <summary>
		/// The selected background color property
		/// </summary>
		public static readonly BindableProperty CancelButtonTextProperty = BindableProperty.Create(nameof(CancelButtonText), typeof(string), typeof(CustomPicker), string.Empty);

		/// <summary>
		/// Gets or sets the cancel button text.
		/// </summary>
		/// <value>
		/// The cancel button text.
		/// </value>
		public string CancelButtonText
		{
			get { return (string)GetValue(CancelButtonTextProperty); }
			set { SetValue(CancelButtonTextProperty, value); }
		}

		/// <summary>
		/// The done button text property
		/// <para>iOS only</para>
		/// </summary>
		public static readonly BindableProperty DoneButtonTextProperty = BindableProperty.Create(nameof(DoneButtonText), typeof(string), typeof(CustomPicker), string.Empty);

		/// <summary>
		/// Gets or sets the done button text.
		/// </summary>
		/// <value>
		/// The done button text.
		/// </value>
		public string DoneButtonText
		{
			get { return (string)GetValue(DoneButtonTextProperty); }
			set { SetValue(DoneButtonTextProperty, value); }
		}
		
		/// <summary>
		/// Bindable Property for the TitleTextColor on Android
		/// </summary>
		public static readonly BindableProperty TitleTextColorProperty = BindableProperty.Create(nameof(TitleTextColor), typeof(Color), typeof(CustomPicker), Color.Default);

		/// <summary>
		/// Gets or sets the TitleTextColor
		/// </summary>
		public Color TitleTextColor
		{
			get { return (Color)GetValue(TitleTextColorProperty); }
			set { SetValue(TitleTextColorProperty, value); }
		}

		/// <summary>
		/// Bindable property for UnderlineColorProperty
		/// </summary>
		public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(CustomPicker), Color.Transparent);

		/// <summary>
		/// Gets or sets the UnderlineColor
		/// </summary>
		public Color UnderlineColor
		{
			get { return (Color)GetValue(UnderlineColorProperty); }
			set { SetValue(UnderlineColorProperty, value); }
		}

		/// <summary>
		/// Bindable property for Padding
		/// </summary>
		public static readonly BindableProperty PaddingProperty = BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(CustomPicker), default(Thickness));

		/// <summary>
		/// Gets or sets the Padding
		/// </summary>
		public Thickness Padding
		{
			get { return (Thickness)GetValue(PaddingProperty); }
			set { SetValue(PaddingProperty, value); }
		}
	}
}
