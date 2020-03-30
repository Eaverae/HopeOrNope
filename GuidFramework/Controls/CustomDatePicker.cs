using Xamarin.Forms;

namespace GuidFramework.Controls
{
    /// <summary>
    /// CustomDatePicker
    /// </summary>
    /// <seealso cref="Xamarin.Forms.DatePicker" />
    public class CustomDatePicker : DatePicker
    {
        /// <summary>
        /// The text alignment bindable property
        /// </summary>
        public static readonly BindableProperty TextAlignmentProperty = BindableProperty.CreateAttached(nameof(TextAlignment), typeof(TextAlignment), typeof(CustomDatePicker), TextAlignment.Start);

        /// <summary>
        /// Gets or sets the text alignment.
        /// </summary>
        /// <value>
        /// The text alignment.
        /// </value>
        public TextAlignment TextAlignment
        {
            get => (TextAlignment)GetValue(TextAlignmentProperty);
            set => SetValue(TextAlignmentProperty, value);
        }

        /// <summary>
        /// Bindable property for UnderlineColorProperty
        /// </summary>
        public static readonly BindableProperty UnderlineColorProperty = BindableProperty.Create(nameof(UnderlineColor), typeof(Color), typeof(CustomDatePicker), Color.Transparent);

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
