using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HopeNope
{
	/// <summary>
	/// Class that contains all the extension methods for an IEnumerable
	/// </summary>
	public static class IEnumerableExtensions
	{
		/// <summary>
		/// Join multiple strings to a single string, using a seperator.
		/// </summary>
		/// <param name="enumeration">The enumerable list of strings.</param>
		/// <param name="separator">The seperator to use.</param>
		/// <returns>A single string joined from the enumeration.</returns>
		public static string Join(this IEnumerable<string> enumeration, string separator)
		{
			if (enumeration == null)
				throw new ArgumentNullException(nameof(enumeration));

			if (separator.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(separator));

			return Join(enumeration, value => value, separator);
		}

		/// <summary>
		/// Join multiple strings to a single string, using a seperator.
		/// </summary>
		/// <param name="enumeration">The enumerable list of strings.</param>
		/// <param name="separator">The seperator to use.</param>
		/// <returns>A single string joined from the enumeration.</returns>
		public static string Join(this IEnumerable<int> enumeration, string separator)
		{
			if (enumeration == null)
				throw new ArgumentNullException(nameof(enumeration));

			if (separator.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(separator));

			return Join(enumeration, value => value.ToString(CultureInfo.InvariantCulture), separator);
		}

		/// <summary>
		/// Join multiple objects to a single string, using a seperator.
		/// </summary>
		/// <typeparam name="T">The type of the object enumeration.</typeparam>
		/// <param name="enumeration">The enumerable list of strings.</param>
		/// <param name="toString">A function (delegate) that converts the object to a string.</param>
		/// <param name="separator">The seperator to use.</param>
		/// <returns>A single string joined from the enumeration.</returns>
		public static string Join<T>(this IEnumerable<T> enumeration, Func<T, string> toString, string separator)
		{
			if (enumeration == null)
				throw new ArgumentNullException(nameof(enumeration));

			if (toString == null)
				throw new ArgumentNullException(nameof(toString));

			if (separator == null)
				throw new ArgumentNullException(nameof(separator));

			return string.Join(separator, enumeration.Select(toString).ToArray());
		}
	}
}
