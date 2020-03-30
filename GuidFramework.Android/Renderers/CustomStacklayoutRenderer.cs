using Android.Content;
using Android.Graphics;
using GuidFramework.Controls;
using GuidFramework.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Xamarin.Forms.Color;

[assembly: ExportRenderer(typeof(CustomStackLayout), typeof(CustomStackLayoutRenderer))]
namespace GuidFramework.Droid.Renderers
{
	/// <summary>
	/// Custom stack layout renderer
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.Android.VisualElementRenderer{StackLayout}" />
	public class CustomStackLayoutRenderer : VisualElementRenderer<StackLayout>
	{
		private CustomStackLayout customStackLayout;
		private Color startColor;
		private Color endColor;

		/// <summary>
		/// Initializes a new instance of the <see cref="CustomStackLayoutRenderer"/> class.
		/// </summary>
		/// <param name="context">The context.</param>
		public CustomStackLayoutRenderer(Context context)
			: base(context)
		{ }

		/// <summary>
		/// Called by draw to draw the child views.
		/// </summary>
		/// <param name="canvas">the canvas on which to draw the view</param>
		/// <remarks>
		/// <para tool="javadoc-to-mdoc">Called by draw to draw the child views. This may be overridden
		/// by derived classes to gain control just before its children are drawn
		/// (but after its own view has been drawn).</para>
		/// <para tool="javadoc-to-mdoc">
		///   <format type="text/html">
		///     <a href="http://developer.android.com/reference/android/view/View.html#dispatchDraw(android.graphics.Canvas)" target="_blank">[Android Documentation]</a>
		///   </format>
		/// </para>
		/// </remarks>
		/// <since version="Added in API level 1" />
		protected override void DispatchDraw(Canvas canvas)
		{
			if (canvas != null)
			{
				float startX = (float)customStackLayout.StartPoint.X;
				float startY = (float)customStackLayout.StartPoint.Y;

				float endX;
				if (customStackLayout.EndPointX < 0)
					endX = (float)canvas.Width;
				else
					endX = (float)customStackLayout.EndPoint.X;

				float endY;
				if (customStackLayout.EndPointY < 0)
					endY = (float)canvas.Height;
				else
					endY = (float)customStackLayout.EndPoint.Y;

				LinearGradient gradient = new LinearGradient(startX, startY, endX, endY, startColor.ToAndroid(), endColor.ToAndroid(), Shader.TileMode.Mirror);

				Paint paint = new Paint
				{
					Dither = true,
					AntiAlias = true
				};
				paint.SetShader(gradient);
				canvas.DrawPaint(paint);
			}

			base.DispatchDraw(canvas);
		}

		/// <summary>
		/// Raises the <see cref="E:ElementChanged" /> event.
		/// </summary>
		/// <param name="e">The <see cref="ElementChangedEventArgs{StackLayout}"/> instance containing the event data.</param>
		protected override void OnElementChanged(ElementChangedEventArgs<StackLayout> e)
		{
			base.OnElementChanged(e);

			if (Element != null && e.NewElement is CustomStackLayout customStackLayout)
			{
				startColor = customStackLayout.StartColor;
				endColor = customStackLayout.EndColor;

				this.customStackLayout = customStackLayout;
			}
		}
	}
}