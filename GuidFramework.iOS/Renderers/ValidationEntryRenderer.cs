using CoreAnimation;
using CoreGraphics;
using GuidFramework.Controls;
using GuidFramework.iOS.Renderers;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ValidationEntry), typeof(ValidationEntryRenderer))]
namespace GuidFramework.iOS.Renderers
{
	/// <summary>
	/// ValidationEntryRenderer
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.iOS.EntryRenderer" />
	public class ValidationEntryRenderer : EntryRenderer
	{
		private CGColor entryUnderlineColor;
		private CGColor entryValidationColor;
		private bool enableValidationUnderline;

		private CALayer underlineBorder;

		/// <summary>
		/// Raises the <see cref="E:ElementChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="ElementChangedEventArgs{Entry}"/> instance containing the event data.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
		{
			base.OnElementChanged(e);

			if (Control != null && Element != null)
			{
				enableValidationUnderline = ((ValidationEntry)Element).EnableValidationUnderline;
				entryUnderlineColor = ((ValidationEntry)Element).UnderlineColor.ToCGColor();
				entryValidationColor = ((ValidationEntry)Element).ValidationErrorColor.ToCGColor();
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
				if (enableValidationUnderline && e != null && (e.PropertyName == ValidationEntry.IsValidProperty.PropertyName) && underlineBorder != null)
					underlineBorder.BorderColor = ((ValidationEntry)Element).IsValid ? entryUnderlineColor : entryValidationColor;
			}
		}

		/// <summary>
		/// Layouts the subviews.
		/// </summary>
		public override void LayoutSubviews()
		{
			DrawUnderline();

			base.LayoutSubviews();
		}

		/// <summary>
		/// Draws the border.
		/// </summary>
		private void DrawUnderline()
		{
			Control.BorderStyle = UITextBorderStyle.None;

			underlineBorder = Control.Layer.Sublayers?.SingleOrDefault(item => item.Name == nameof(underlineBorder)) ?? null;

			// Border and underline
			if (underlineBorder == null)
			{
				underlineBorder = new CALayer();
				underlineBorder.Name = nameof(underlineBorder);

				Control.Layer.AddSublayer(underlineBorder);
			}

			underlineBorder.MasksToBounds = true;
			underlineBorder.Frame = new CGRect(0f, Frame.Height, Frame.Width, 1f);
			underlineBorder.BorderColor = entryUnderlineColor;
			underlineBorder.BorderWidth = 1.0f;

			CALayer currentlayer = Control.Layer.Sublayers.FirstOrDefault();

			if (currentlayer != null)
				Control.Layer.ReplaceSublayer(currentlayer, underlineBorder);
		}
	}
}