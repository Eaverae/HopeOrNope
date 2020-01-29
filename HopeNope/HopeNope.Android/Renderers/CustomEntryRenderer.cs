using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Text.Method;
using HopeNope.Controls;
using HopeNope.Droid.Renderers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace HopeNope.Droid.Renderers
{
	public class CustomEntryRenderer : EntryRenderer
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ValidationEntryRenderer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public CustomEntryRenderer(Context context) : base(context) { }

		/// <summary>
		/// Raises the <see cref="E:ElementChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="ElementChangedEventArgs{Entry}"/> instance containing the event data.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null && Element != null)
			{
				CustomEntry entry = (CustomEntry)Element;

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
				
				// Numeric fix
				if (entry.Keyboard == Keyboard.Numeric)
					Control.KeyListener = DigitsKeyListener.GetInstance("1234567890.,");
			}
		}

		/// <summary>
		/// Creates the background.
		/// </summary>
		/// <param name="entry">The entry.</param>
		/// <returns>A drawable</returns>
		private static Drawable CreateBackground(CustomEntry entry)
		{
			if (entry == null)
				throw new ArgumentNullException(nameof(entry));

			Drawable[] drawables = new Drawable[2];

			ShapeDrawable underline = new ShapeDrawable(new RectShape());
			ShapeDrawable background = new ShapeDrawable(new RectShape());

			underline.Paint.Color = entry.UnderlineColor.ToAndroid();

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