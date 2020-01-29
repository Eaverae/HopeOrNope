using Xamarin.Forms;

namespace HopeNope.Controls
{
	/// <summary>
	/// Custom stack layout
	/// </summary>
	public class CustomStackLayout : StackLayout
	{
		/// <summary>
		/// The start point property
		/// </summary>
		public static readonly BindableProperty StartColorProperty = BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(CustomStackLayout));

		/// <summary>
		/// Gets or sets the start color.
		/// </summary>
		/// <value>
		/// The start color.
		/// </value>
		public Color StartColor
		{
			get => (Color)GetValue(StartColorProperty);
			set => SetValue(StartColorProperty, value);
		}

		/// <summary>
		/// The start point property
		/// </summary>
		public static readonly BindableProperty EndColorProperty = BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(CustomStackLayout));

		/// <summary>
		/// Gets or sets the end color.
		/// </summary>
		/// <value>
		/// The end color.
		/// </value>
		public Color EndColor
		{
			get => (Color)GetValue(EndColorProperty);
			set => SetValue(EndColorProperty, value);
		}

		/// <summary>
		/// The start point property
		/// </summary>
		public static readonly BindableProperty StartPointXProperty = BindableProperty.Create(nameof(StartPointX), typeof(double), typeof(CustomStackLayout), defaultValue: -1.0, propertyChanged: StartPointXPropertyChanged);

		/// <summary>
		/// Gets or sets the start point.
		/// </summary>
		/// <value>
		/// The start point.
		/// </value>
		public double StartPointX
		{
			get => (double)GetValue(StartPointXProperty);
			set => SetValue(StartPointXProperty, value);
		}

		/// <summary>
		/// The start point property
		/// </summary>
		public static readonly BindableProperty StartPointYProperty = BindableProperty.Create(nameof(StartPointY), typeof(double), typeof(CustomStackLayout), defaultValue: -1.0, propertyChanged: StartPointYPropertyChanged);

		/// <summary>
		/// Gets or sets the start point.
		/// </summary>
		/// <value>
		/// The start point.
		/// </value>
		public double StartPointY
		{
			get => (double)GetValue(StartPointYProperty);
			set => SetValue(StartPointYProperty, value);
		}

		/// <summary>
		/// The start point property
		/// </summary>
		public static readonly BindableProperty StartPointProperty = BindableProperty.Create(nameof(StartPoint), typeof(Point), typeof(CustomStackLayout));

		/// <summary>
		/// Gets or sets the start point.
		/// </summary>
		/// <value>
		/// The start point.
		/// </value>
		public Point StartPoint
		{
			get => (Point)GetValue(StartPointProperty);
			set => SetValue(StartPointProperty, value);
		}

		/// <summary>
		/// The end point property
		/// </summary>
		public static readonly BindableProperty EndPointProperty = BindableProperty.Create(nameof(EndPoint), typeof(Point), typeof(CustomStackLayout));

		/// <summary>
		/// Gets or sets the end point.
		/// </summary>
		/// <value>
		/// The end point.
		/// </value>
		public Point EndPoint
		{
			get => (Point)GetValue(EndPointProperty);
			set => SetValue(EndPointProperty, value);
		}

		/// <summary>
		/// The end point property
		/// </summary>
		public static readonly BindableProperty EndPointXProperty = BindableProperty.Create(nameof(EndPointX), typeof(double), typeof(CustomStackLayout), defaultValue: -1.0, propertyChanged: EndPointXPropertyChanged);


		/// <summary>
		/// Gets or sets the end point.
		/// </summary>
		/// <value>
		/// The end point.
		/// </value>
		public double EndPointX
		{
			get => (double)GetValue(EndPointXProperty);
			set => SetValue(EndPointXProperty, value);
		}

		/// <summary>
		/// The end point property
		/// </summary>
		public static readonly BindableProperty EndPointYProperty = BindableProperty.Create(nameof(EndPointY), typeof(double), typeof(CustomStackLayout), defaultValue: -1.0, propertyChanged: EndPointYPropertyChanged);

		/// <summary>
		/// Gets or sets the end point.
		/// </summary>
		/// <value>
		/// The end point.
		/// </value>
		public double EndPointY
		{
			get => (double)GetValue(EndPointYProperty);
			set => SetValue(EndPointYProperty, value);
		}

		/// <summary>
		/// Starts the point x property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void StartPointXPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			CustomStackLayout customStacklayout = (bindable as CustomStackLayout);

			if (customStacklayout != null)
				customStacklayout.StartPoint = new Point((double)newValue, customStacklayout.StartPoint.Y);
		}

		/// <summary>
		/// Starts the point y property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void StartPointYPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			CustomStackLayout customStacklayout = (bindable as CustomStackLayout);

			if (customStacklayout != null)
				customStacklayout.StartPoint = new Point(customStacklayout.StartPoint.X, (double)newValue);
		}

		/// <summary>
		/// Ends the point x property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void EndPointXPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			CustomStackLayout customStacklayout = (bindable as CustomStackLayout);

			if (customStacklayout != null)
				customStacklayout.EndPoint = new Point((double)newValue, customStacklayout.EndPoint.Y);
		}

		/// <summary>
		/// Ends the point y property changed.
		/// </summary>
		/// <param name="bindable">The bindable.</param>
		/// <param name="oldValue">The old value.</param>
		/// <param name="newValue">The new value.</param>
		private static void EndPointYPropertyChanged(BindableObject bindable, object oldValue, object newValue)
		{
			CustomStackLayout customStacklayout = (bindable as CustomStackLayout);

			if (customStacklayout != null)
				customStacklayout.EndPoint = new Point(customStacklayout.EndPoint.X, (double)newValue);
		}
	}
}