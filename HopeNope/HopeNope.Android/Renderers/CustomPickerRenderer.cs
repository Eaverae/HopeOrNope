using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Graphics.Drawables.Shapes;
using Android.OS;
using Android.Widget;
using HopeNope.Controls;
using HopeNope.Droid.Renderers;
using System;
using System.ComponentModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Views.View;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace HopeNope.Droid.Renderers
{
	/// <summary>
	/// CustomPickerRenderer
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.Android.PickerRenderer" />
	/// <seealso cref="Android.Views.View.IOnClickListener" />
	public class CustomPickerRenderer : PickerRenderer, IOnClickListener
	{
		private IElementController ElementController => Element as IElementController;
		private AlertDialog.Builder dialogBuilder;
		private AlertDialog dialog;

		private CustomPicker customPicker;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomPickerRenderer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public CustomPickerRenderer(Context context) : base(context) { }

		/// <summary>
		/// Raises the <see cref="E:ElementChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="ElementChangedEventArgs{Picker}"/> instance containing the event data.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
		{
			base.OnElementChanged(e);

			if (e.NewElement != null)
			{
				customPicker = e.NewElement as CustomPicker;

				if (customPicker != null)
				{
					Control.SetOnClickListener(this);

					// Set the TitleTextColor if it's set; otherwise it defaults to the TitleColor property
					if (customPicker.TitleTextColor != Xamarin.Forms.Color.Default)
						Control.SetHintTextColor(customPicker.TitleTextColor.ToAndroid());

					// Better underline with non-transparent background
					if (customPicker.BackgroundColor != Xamarin.Forms.Color.Transparent)
						Control.Background = CreateBackground(customPicker);
					else
					{
						// Underline
						if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
							Control.BackgroundTintList = ColorStateList.ValueOf(customPicker.UnderlineColor.ToAndroid());
						else
							Control.Background.SetColorFilter(customPicker.UnderlineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
					}
				}
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
				CustomPicker picker = (CustomPicker)Element;

				if (picker.BackgroundColor != Xamarin.Forms.Color.Transparent)
					Control.Background = CreateBackground(picker);

				Control.Background.SetTint(picker.UnderlineColor.ToAndroid());
			}
		}

		/// <summary>
		/// Creates the background.
		/// </summary>
		/// <param name="picker">The entry.</param>
		/// <returns>A drawable</returns>
		private static Drawable CreateBackground(CustomPicker picker)
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

		/// <summary>
		/// Called when [click].
		/// </summary>
		/// <param name="view">The view.</param>
		public void OnClick(Android.Views.View view)
		{
			Picker model = Element;
			NumberPicker picker = new NumberPicker(Context);

			if (model.Items != null && model.Items.Any())
			{
				picker.MaxValue = model.Items.Count - 1;
				picker.MinValue = 0;
				picker.SetDisplayedValues(model.Items.ToArray());
				picker.WrapSelectorWheel = false;
				picker.DescendantFocusability = Android.Views.DescendantFocusability.BlockDescendants;
				picker.Value = model.SelectedIndex;
			}

			LinearLayout layout = new LinearLayout(Context) { Orientation = Android.Widget.Orientation.Vertical };
			layout.AddView(picker);

			ElementController.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, true);

			// only construct a builder class when needed.
			if (dialogBuilder == null)
				dialogBuilder = new AlertDialog.Builder(Context);

			dialogBuilder.SetView(layout);
			dialogBuilder.SetTitle(model.Title ?? string.Empty);

			dialogBuilder.SetNegativeButton(customPicker.CancelButtonText ?? "Cancel", (s, a) =>
			{
				ElementController.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
				dialog = null;
			});
			dialogBuilder.SetPositiveButton(customPicker.AcceptButtonText ?? "Accept", (s, a) =>
			{
				ElementController.SetValueFromRenderer(Picker.SelectedIndexProperty, picker.Value);
				// It is possible for the Content of the Page to be changed on SelectedIndexChanged. 
				// In this case, the Element & Control will no longer exist.
				if (Element != null)
				{
					if (model.Items.Count > 0 && Element.SelectedIndex >= 0)
						Control.Text = model.Items[Element.SelectedIndex];
					ElementController.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
				}
				dialog = null;
			});

			dialog = dialogBuilder.Create();
			dialog.DismissEvent += (sender, args) =>
			{
				ElementController?.SetValueFromRenderer(VisualElement.IsFocusedPropertyKey, false);
			};
			dialog.Show();
		}
	}
}