using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Views;
using Android.Widget;
using GuidFramework.Controls;
using GuidFramework.Droid.Renderers;
using System;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace GuidFramework.Droid.Renderers
{
	/// <summary>
	/// CustomDatePickerRenderer
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.Android.EntryRenderer" />
	public class CustomDatePickerRenderer : DatePickerRenderer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CustomDatePickerRenderer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public CustomDatePickerRenderer(Context context) : base(context) { }

		/// <summary>
		/// Raises the <see cref="E:ElementChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="ElementChangedEventArgs{Entry}"/> instance containing the event data.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
		{
			base.OnElementChanged(e);

			if (Control != null && Element != null)
			{
				CustomDatePicker picker = (CustomDatePicker)Element;

				// Better underline with non-transparent background
				if (picker.BackgroundColor != Xamarin.Forms.Color.Transparent)
					Control.Background = CreateBackground(picker);
				else
				{
					// Underline
					if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
						Control.BackgroundTintList = ColorStateList.ValueOf(picker.UnderlineColor.ToAndroid());
					else
						Control.Background.SetColorFilter(picker.UnderlineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
				}

				SetTextAlignment(picker);
			}
		}

		/// <summary>
		/// Sets the alignment.
		/// </summary>
		/// <param name="picker">The picker.</param>
		private void SetTextAlignment(CustomDatePicker picker)
		{
			GravityFlags gravityFlags;

			switch (picker.TextAlignment)
			{
				case Xamarin.Forms.TextAlignment.Center:
					gravityFlags = GravityFlags.CenterHorizontal;
					break;
				case Xamarin.Forms.TextAlignment.End:
					gravityFlags = GravityFlags.End;
					break;
				default:
					gravityFlags = GravityFlags.Start;
					break;
			}

			Control.Gravity = gravityFlags;
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
				CustomDatePicker picker = (CustomDatePicker)Element;

				(Control as EditText).Background.SetTint(picker.UnderlineColor.ToAndroid());
			}
		}

		/// <summary>
		/// Creates the background.
		/// </summary>
		/// <param name="picker">The picker.</param>
		/// <returns>A drawable</returns>
		private static Drawable CreateBackground(CustomDatePicker picker)
		{
			if (picker == null)
				throw new ArgumentNullException(nameof(picker));

			Drawable[] drawables = new Drawable[2];

			ShapeDrawable underline = new ShapeDrawable(new RectShape());
			ShapeDrawable background = new ShapeDrawable(new RectShape());

			underline.Paint.Color = picker.UnderlineColor.ToAndroid();

			underline.Paint.SetStyle(Paint.Style.Fill);
			underline.SetPadding(0, 0, 0, 5);

			background.Paint.Alpha = 0;
			background.Paint.Color = picker.BackgroundColor.ToAndroid();
			background.Paint.SetStyle(Paint.Style.Fill);

			drawables[0] = underline;
			drawables[1] = background;

			return new LayerDrawable(drawables);
		}
	}
}