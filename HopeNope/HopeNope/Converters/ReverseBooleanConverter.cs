using System;
using System.Globalization;
using Xamarin.Forms;

namespace HopeNope.Converters
{
	/// <summary>
	/// Converts a boolean's value to the opposite value
	/// </summary>
	/// <seealso cref="Xamarin.Forms.IValueConverter" />
	public class ReverseBooleanConverter : IValueConverter
	{
		/// <summary>
		/// Implement this method to convert <paramref name="value" /> to <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		/// <exception cref="ArgumentNullException">value</exception>
		/// <exception cref="InvalidOperationException">value</exception>
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertBoolean(value);
		}

		/// <summary>
		/// Implement this method to convert <paramref name="value" /> back from <paramref name="targetType" /> by using <paramref name="parameter" /> and <paramref name="culture" />.
		/// </summary>
		/// <param name="value">The value to convert.</param>
		/// <param name="targetType">The type to which to convert the value.</param>
		/// <param name="parameter">A parameter to use during the conversion.</param>
		/// <param name="culture">The culture to use during the conversion.</param>
		/// <exception cref="ArgumentNullException">value</exception>
		/// <exception cref="InvalidOperationException">value</exception>
		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			return ConvertBoolean(value);
		}

		/// <summary>
		/// Converts the boolean.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">value</exception>
		/// <exception cref="InvalidOperationException">value</exception>
		private static bool ConvertBoolean(object value)
		{
			if (value == null)
				throw new ArgumentNullException(nameof(value));

			if (!(value is bool))
				throw new InvalidOperationException(nameof(value));

			return !(bool)value;
		}
	}
}
