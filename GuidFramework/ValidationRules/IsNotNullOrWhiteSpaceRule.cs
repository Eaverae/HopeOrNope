using GuidFramework.Extensions;
using GuidFramework.Interfaces;

namespace GuidFramework.ValidationRules
{
	/// <summary>
	/// IsNullOrWhiteSpaceRule
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="GuidFramework.Interfaces.IValidationRule{T}" />
	public class IsNullOrWhiteSpaceRule<T> : IValidationRule<T>
	{
		/// <summary>
		/// Gets or sets the validation message.
		/// </summary>
		/// <value>
		/// The validation message.
		/// </value>
		public string ValidationMessage { get; set; }

		/// <summary>
		/// Checks the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>A boolean value</returns>
		public bool Check(T value)
		{
			bool result = false;

			if (value != null)
			{
				string stringValue = value as string;

				result = !stringValue.IsNullOrWhiteSpace();
			}

			return result;
		}
	}
}
