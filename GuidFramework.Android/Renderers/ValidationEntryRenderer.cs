using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using GuidFramework.Extensions;
using GuidFramework.Android.Classes;
using GuidFramework.Controls;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using System.ComponentModel;
using Android.Text.Method;
using GuidFramework.Android.Renderers;

[assembly: ExportRenderer(typeof(ValidationEntry), typeof(ValidationEntryRenderer))]
namespace GuidFramework.Android.Renderers
{
	/// <summary>
	/// ValidationEntryRenderer
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.Android.EntryRenderer" />
	public class ValidationEntryRenderer : EntryRenderer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationEntryRenderer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public ValidationEntryRenderer(Context context) : base(context) { }

		/// <summary>
		/// Raises the <see cref="E:ElementChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="ElementChangedEventArgs{Entry}"/> instance containing the event data.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null && Element != null)
			{
				ValidationEntry entry = (ValidationEntry)Element;

				// Better underline with non-transparent background
				if (entry.BackgroundColor != Xamarin.Forms.Color.Transparent)
					Control.Background = CreateBackground(entry);
				else
				{
					// Underline
					if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
						Control.BackgroundTintList = ColorStateList.ValueOf(entry.UnderlineColor.ToAndroid());
					else
						Control.Background.SetColorFilter(entry.UnderlineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
				}

				// Font Family
				if (!entry.FontFamily.IsNullOrWhiteSpace())
					Control.Typeface = FontCache.Instance.FontForName(Context, entry.FontFamily);

				// Numeric fix
				if (entry.Keyboard == Keyboard.Numeric)
					Control.KeyListener = DigitsKeyListener.GetInstance("1234567890.,");
			}
		}

		/// <summary>
		/// Called when [element property changed].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (Control != null && Element != null)
			{
				ValidationEntry entry = (ValidationEntry)Element;

				if (entry.EnableValidationUnderline && e != null && (e.PropertyName == ValidationEntry.IsValidProperty.PropertyName))
				{
					if (entry.BackgroundColor != Xamarin.Forms.Color.Transparent)
						Control.Background = CreateBackground(entry);
					else
					{
						if (entry.IsValid)
							Control.Background.SetTint(entry.UnderlineColor.ToAndroid());
						else
							Control.Background.SetTint(entry.ValidationErrorColor.ToAndroid());
					}
				}
			}
		}

		/// <summary>
		/// Creates the background.
		/// </summary>
		/// <param name="entry">The entry.</param>
		/// <returns>A drawable</returns>
		private static Drawable CreateBackground(ValidationEntry entry)
		{
			if (entry == null)
				throw new ArgumentNullException(nameof(entry));

			Drawable[] drawables = new Drawable[2];

			ShapeDrawable underline = new ShapeDrawable(new RectShape());
			ShapeDrawable background = new ShapeDrawable(new RectShape());

			if (entry.IsValid)
				underline.Paint.Color = entry.UnderlineColor.ToAndroid();
			else
				underline.Paint.Color = entry.ValidationErrorColor.ToAndroid();

			underline.Paint.SetStyle(Paint.Style.Fill);
			underline.SetPadding(0, 0, 0, 5);

			background.Paint.Alpha = 0;
			background.Paint.Color = entry.BackgroundColor.ToAndroid();
			background.Paint.SetStyle(Paint.Style.Fill);

			drawables[0] = underline;
			drawables[1] = background;

			return new LayerDrawable(drawables);
		}
	}
}