using CoreAnimation;
using CoreGraphics;
using HopeNope.Controls;
using HopeNope.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomStackLayout), typeof(CustomStackLayoutRenderer))]
namespace HopeNope.iOS.Renderers
{
	/// <summary>
	/// Custom StackLayout Renderer
	/// </summary>
	/// <seealso cref="StackLayout" />
	public class CustomStackLayoutRenderer : VisualElementRenderer<StackLayout>
	{
		/// <summary>
		/// Draws the view within the passed-in rectangle.
		/// </summary>
		/// <param name="rectangle">The <see cref="T:System.Drawing.RectangleF" /> to draw.</param>
		public override void Draw(CGRect rectangle)
		{
			base.Draw(rectangle);

			if (Element is CustomStackLayout customStackLayout)
			{
				float startX = (float)customStackLayout.StartPoint.X;
				float startY = (float)customStackLayout.StartPoint.Y;

				float endX;
				if (customStackLayout.EndPointX < 0)
					endX = 1;
				else
					endX = (float)customStackLayout.EndPoint.X / (float)rectangle.Width;

				float endY;
				if (customStackLayout.EndPointY < 0)
					endY = 1;
				else
					endY = (float)customStackLayout.EndPoint.Y / (float)rectangle.Height;

				CGColor startColor = customStackLayout.StartColor.ToCGColor();
				CGColor endColor = customStackLayout.EndColor.ToCGColor();

				CAGradientLayer gradientLayer = new CAGradientLayer
				{
					Frame = rectangle,
					StartPoint = new CGPoint(startX, startY),
					EndPoint = new CGPoint(endX, endY),
					Colors = new[] { startColor, endColor }
				};

				NativeView.Layer.InsertSublayer(gradientLayer, 0);
			}
		}
	}
}