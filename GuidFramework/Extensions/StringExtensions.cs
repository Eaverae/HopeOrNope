using System;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;

namespace GuidFramework.Extensions
{
	/// <summary>
	/// String extensions
	/// </summary>
	public static class StringExtensions
	{
		private static readonly char[] padding = { '=' };

		/// <summary>
		/// Url safe encode
		/// </summary>
		/// <param name="base64String">Base 64 string</param>
		/// <returns>Safe url encoded string</returns>
		public static string UrlSafeEncode(this string base64String)
		{
			return base64String.TrimEnd(padding).Replace('+', '-').Replace('/', '_');
		}

		/// <summary>
		/// Url safe decode
		/// </summary>
		/// <param name="urlSafeBase64String">Url safe base 64 string</param>
		/// <returns>Safe url decoded string</returns>
		public static string UrlSafeDecode(this string urlSafeBase64String)
		{
			string base64String = urlSafeBase64String.Replace('_', '/').Replace('-', '+');
			switch (urlSafeBase64String.Length % 4)
			{
				case 2:
					base64String += "==";
					break;
				case 3:
					base64String += "=";
					break;
			}
			return base64String;
		}
		/// <summary>
		/// Returns a boolean value that indicates whether or not the current instance is null, string.empty or only contains whitespace characters
		/// </summary>
		/// <param name="instance">The instance to verify</param>
		/// <returns>A boolean value</returns>
		public static bool IsNullOrWhiteSpace(this string instance)
		{
			return string.IsNullOrWhiteSpace(instance);
		}

		/// <summary>
		/// Returns a boolean value that indicates whether or not the current instance is null or string.empty
		/// </summary>
		/// <param name="instance">The instance to verify</param>
		/// <returns>A boolean value</returns>
		public static bool IsNullOrEmpty(this string instance)
		{
			return string.IsNullOrEmpty(instance);
		}

		/// <summary>
		/// Changes the first character of the string to uppercase
		/// </summary>
		/// <param name="instance">The string to work on</param>
		/// <returns>The string with an uppercased first character</returns>
		public static string FirstToUpper(this string instance)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			string result;
			if (!string.IsNullOrEmpty(instance))
			{
				char[] charArray = instance.ToCharArray();
				charArray[0] = char.ToUpper(charArray[0]);
				result = new string(charArray);
			}
			else
				result = instance;

			return result;
		}

		/// <summary>
		/// Changes the first character of the string to lowercase
		/// </summary>
		/// <param name="instance">The string to work on</param>
		/// <returns>The string with a lowercase first character</returns>
		public static string FirstToLower(this string instance)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			string result;
			if (!string.IsNullOrEmpty(instance))
			{
				char[] charArray = instance.ToCharArray();
				charArray[0] = char.ToLower(charArray[0]);
				result = new string(charArray);
			}
			else
				result = instance;

			return result;
		}

		/// <summary>
		/// Replaces each format item in a specified string with the text equivalent of a corresponding object's value.
		/// <para>This method formats the string object with an InvariantCulture</para>
		/// </summary>
		/// <param name="instance">The formatstring</param>
		/// <param name="arguments">The arguments</param>
		/// <returns>The formatted string</returns>
		public static string FormatInvariant(this string instance, params object[] arguments)
		{
			if (instance.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(instance));

			if (arguments == null)
				throw new ArgumentNullException(nameof(arguments));

			return string.Format(CultureInfo.InvariantCulture, instance, arguments);
		}

		/// <summary>
		/// Detects when a string contains invalid Text characters such as 😍😘🙂🙂😇🏚
		/// </summary>
		/// <para>Please note: The following characters are also not allowed:</para>
		/// <para>+=~`^</para>
		/// <param name="instance">The instance to check</param>
		/// <returns>A boolean value</returns>
		public static bool ContainsInvalidTextCharacters(this string instance)
		{
			bool returnValue = false;

			if (!instance.IsNullOrWhiteSpace())
			{
				Regex regex = new Regex(@"^[\p{P}\p{Sc}\w\d\s]*$", RegexOptions.Compiled);

				returnValue = !regex.IsMatch(instance);
			}

			return returnValue;
		}

		/// <summary>
		/// Removes all special characters from a string
		/// <para>Please note that Spaces will be left intact</para>
		/// </summary>
		/// <example>A string like !@#%$!abc123#% becomes abc123 without a replacementString.
		/// <para>If the replacementString is "_" the previous example becomes ______abc123__</para>
		/// </example>
		/// <param name="instance">The string instance to use</param>
		/// <param name="replacementValue">The string to replace the special characters with</param>
		/// <param name="validCharacters">A list of characters that will ignored</param>
		/// <returns>A string with the special characters removed</returns>
		public static string RemoveSpecialCharacters(this string instance, char? replacementValue = null, params char[] validCharacters)
		{
			if (instance.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(instance));

			string validCharacterString = string.Empty;

			// Add whitelisted characters to the whiteList
			// These will be included in the regex
			// Use the escape method to ensure the correct characters
			if (validCharacters != null && validCharacters.Length > 0)
				validCharacterString = Regex.Escape(validCharacters.Join(item => item.ToString(), string.Empty));

			Regex regex = new Regex(@"([a-zA-Z0-9 {0}]*)(.?)".FormatInvariant(validCharacterString), RegexOptions.None);

			string returnValue = replacementValue == null ? regex.Matches(instance).Cast<Match>().Join(item => item.Groups[1].Value, string.Empty) :
															regex.Matches(instance).Cast<Match>().Join(item => item.Groups[1].Value + new string((char)replacementValue, item.Groups[2].Value.Length), string.Empty);


			return returnValue;
		}


		/// <summary>
		/// Checks if the input string is a valid e-mail address
		/// </summary>
		/// <param name="input">The input string</param>
		/// <returns>True if the input string is a valid e-mail address</returns>
		public static bool IsEmailAddress(this string input)
		{
			if (input == null)
				throw new ArgumentNullException(nameof(input));

			const string emailStrictPattern = @"^(([^<>()[\]\\.,;:\s@\""]+"
											  + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
											  + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
											  + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
											  + @"[a-zA-Z]{2,}))$";

			Regex expression = new Regex(emailStrictPattern);
			return expression.IsMatch(input);
		}

		/// <summary>
		/// Checks if the input string is a valid URL
		/// </summary>
		/// <param name="input">The input string</param>
		/// <returns>True if the input string is a valid URL</returns>
		public static bool IsUrl(this string input)
		{
			if (input == null)
				throw new ArgumentNullException(nameof(input));

			const string urlPattern = @"((www\.|(http|https|ftp|news|file)+\:\/\/)[&#95;.a-z0-9-]+\.[a-z0-9\/&#95;:@=.+?,##%&~-]*[^.|\'|\# |!|\(|?|,| |>|<|;|\)])";

			Regex expression = new Regex(urlPattern);
			return expression.IsMatch(input);
		}

		/// <summary>
		/// Alternative for string.IndexOf with the CurrentCulture
		/// </summary>
		/// <param name="instance">The string.</param>
		/// <param name="find">The string to seek</param>
		/// <returns>The index of a found string using the CurrentCulture</returns>
		public static int IndexOfCurrent(this string instance, string find)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			return instance.IndexOf(find, StringComparison.CurrentCulture);
		}

		/// <summary>
		/// Alternative for string.StartsWith with the CurrentCulture
		/// </summary>
		/// <param name="instance">The string.</param>
		/// <param name="compare">The string to compare to.</param>
		/// <returns>True, if the value start with a specific string.</returns>
		public static bool StartsWithCurrent(this string instance, string compare)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			return instance.StartsWith(compare, StringComparison.CurrentCulture);
		}

		/// <summary>
		/// Alternative for string.StartsWith with the CurrentCulture
		/// </summary>
		/// <param name="instance">The string.</param>
		/// <param name="compare">The string to compare to.</param>
		/// <returns>True, if the value start with a specific string.</returns>
		public static bool EndsWithCurrent(this string instance, string compare)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			return instance.EndsWith(compare, StringComparison.CurrentCulture);
		}

		/// <summary>
		/// Gets only the numbers from the string (concatenated)
		/// </summary>
		/// <param name="instance">The string to get the numbers from</param>
		/// <returns>A concatenated string of all the numbers in the string instance</returns>
		public static string GetNumbers(this string instance)
		{
			if (instance == null)
				throw new ArgumentNullException(nameof(instance));

			return Regex.Replace(instance, @"\D", string.Empty);
		}

		/// <summary>
		/// Removes all the numeric characters from a string
		/// </summary>
		/// <param name="instance">The string instance to extend</param>
		/// <returns>A string that contains no numeric characters</returns>
		public static string RemoveNumericCharacters(this string instance)
		{
			if (instance.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(instance));

			string returnValue = Regex.Replace(instance, @"[\d]*", string.Empty);

			return returnValue;
		}

		/// <summary>
		/// Converts the string to uppercase with invariant culture.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>Uppercase string.</returns>
		/// <exception cref="ArgumentNullException"></exception>
		public static string ToUpperInvariant(this string instance)
		{
			if (instance.IsNullOrWhiteSpace())
				throw new ArgumentNullException();

			string returnValue = instance.ToUpper(CultureInfo.InvariantCulture);

			return returnValue;
		}

		/// <summary>
		/// Converts the string to a XmlDocument.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns>XmlDocument.</returns>
		public static XmlDocument ToXmlDocument(this string instance)
		{
			XmlDocument xmlDocument = new XmlDocument();

			xmlDocument.LoadXml(instance);

			return xmlDocument;
		}
	}
}
