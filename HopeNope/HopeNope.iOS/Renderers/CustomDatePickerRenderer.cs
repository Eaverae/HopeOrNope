using CoreAnimation;
using CoreGraphics;
using HopeNope.Controls;
using HopeNope.iOS.Renderers;
using System.ComponentModel;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomDatePicker), typeof(CustomDatePickerRenderer))]
namespace HopeNope.iOS.Renderers
{
	/// <summary>
	/// CustomEntryRenderer
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.iOS.DatePickerRenderer" />
	public class CustomDatePickerRenderer : DatePickerRenderer
	{
		private CALayer borderLayer;
		private CustomDatePicker picker;

		/// <summary>
		/// Raises the <see cref="E:ElementChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="ElementChangedEventArgs{DatePicker}"/> instance containing the event data.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
		{
			base.OnElementChanged(e);

			if (Control != null && Element != null)
			{
				picker = (CustomDatePicker)Element;
				SetTextAlignment();
			}
		}

		/// <summary>
		/// Sets the text alignment.
		/// </summary>
		private void SetTextAlignment()
		{
			switch (picker.TextAlignment)
			{
				case TextAlignment.Center:
					Control.TextAlignment = UITextAlignment.Center;
					break;
				case TextAlignment.End:
					Control.TextAlignment = UITextAlignment.Right;
					break;
				default:
					Control.TextAlignment = UITextAlignment.Left;
					break;
			}
		}

		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews()
		{
			if (picker != null)
				DrawBorder(picker);

			base.LayoutSubviews();
		}

		/// <summary>
		/// Draws the border.
		/// </summary>
		/// <param name="picker">The entry.</param>
		private void DrawBorder(CustomDatePicker picker)
		{
			// Border and underline
			borderLayer = new CALayer();
			borderLayer.MasksToBounds = true;
			borderLayer.Frame = new CGRect(0f, Frame.Height, Frame.Width, 1f);

			borderLayer.BorderColor = picker.UnderlineColor.ToCGColor();

			borderLayer.BorderWidth = 1.0f;

			Control.Layer.AddSublayer(borderLayer);
			Control.BorderStyle = UITextBorderStyle.None;
		}

		/// <summary>
		/// Called when [element property changed].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="PropertyChangedEventArgs"/> instance containing the event data.</param>
		protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			base.OnElementPropertyChanged(sender, e);

			if (borderLayer != null && picker != null)
				borderLayer.BorderColor = picker.UnderlineColor.ToCGColor();
		}
	}
}