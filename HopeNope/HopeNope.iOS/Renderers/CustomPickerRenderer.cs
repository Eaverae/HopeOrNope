using CoreAnimation;
using CoreGraphics;
using Foundation;
using HopeNope.Controls;
using HopeNope.iOS.Renderers;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomPicker), typeof(CustomPickerRenderer))]
namespace HopeNope.iOS.Renderers
{
	/// <summary>
	/// CustomPickerRenderer
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.iOS.PickerRenderer" />
	public class CustomPickerRenderer : PickerRenderer
	{
		private CGColor entryUnderlineColor;

		private CALayer underlineBorder;
		private CustomPicker customPicker;

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

				if (Control == null)
				{
					SetNativeControl(new UITextField
					{
						RightViewMode = UITextFieldViewMode.Always,
						ClearButtonMode = UITextFieldViewMode.WhileEditing,
					});
					SetUIButton(customPicker.DoneButtonText);
				}
				else
					SetUIButton(customPicker.DoneButtonText);
			}

			if (Control != null && customPicker != null)
			{
				entryUnderlineColor = customPicker.UnderlineColor.ToCGColor();

				// Set the TitleTextColor if it's set; otherwise it defaults to the TitleColor property
				if (((CustomPicker)Element).TitleTextColor != Color.Default && Control.AttributedPlaceholder != null)
					Control.AttributedPlaceholder = new NSAttributedString(Control.AttributedPlaceholder.Value, foregroundColor: customPicker.TitleTextColor.ToUIColor());

				Control.BorderStyle = UITextBorderStyle.None;
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

			if (underlineBorder != null && customPicker != null)
				underlineBorder.BorderColor = customPicker.UnderlineColor.ToCGColor();
		}

		/// <summary>
		/// Sets the UI button.
		/// </summary>
		/// <param name="doneButtonText">The done button text.</param>
		public void SetUIButton(string doneButtonText)
		{
			UIToolbar toolbar = new UIToolbar();
			toolbar.BarStyle = UIBarStyle.Default;
			toolbar.Translucent = true;
			toolbar.SizeToFit();
			UIBarButtonItem doneButton = new UIBarButtonItem(doneButtonText.IsNullOrWhiteSpace() ? "Accept" : doneButtonText, UIBarButtonItemStyle.Done, (s, e) =>
			{
				Control.ResignFirstResponder();
			});
			UIBarButtonItem flexible = new UIBarButtonItem(UIBarButtonSystemItem.FlexibleSpace);
			toolbar.SetItems(new UIBarButtonItem[] { flexible, doneButton }, true);
			Control.InputAccessoryView = toolbar;
		}

		/// <summary>
		/// Draws the view within the passed-in rectangle.
		/// </summary>
		/// <param name="rect">The rectangle</param>
		public override void Draw(CGRect rect)
		{
			base.Draw(rect);

			DrawUnderline();
		}

		/// <summary>
		/// Draws the border.
		/// </summary>
		private void DrawUnderline()
		{
			underlineBorder = Control.Layer.Sublayers?.SingleOrDefault(item => item.Name == nameof(underlineBorder)) ?? null;

			// Border and underline
			if (underlineBorder == null)
			{
				underlineBorder = new CALayer();
				underlineBorder.Name = nameof(underlineBorder);

				Control.Layer.AddSublayer(underlineBorder);
			}

			underlineBorder.MasksToBounds = true;
			underlineBorder.Frame = new CGRect(0f, Frame.Height + 5, Frame.Width, 1f);
			underlineBorder.BorderColor = entryUnderlineColor;
			underlineBorder.BorderWidth = 1.0f;
		}
	}
}