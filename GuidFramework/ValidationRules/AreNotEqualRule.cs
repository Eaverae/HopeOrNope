using GuidFramework.Interfaces;

namespace GuidFramework.ValidationRules
{
	/// <summary>
	/// AreNotEqualRule
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="GuidFramework.Interfaces.IValidationRule{T}" />
	public class AreNotEqualRule<T> : IValidationRule<T>
	{
		/// <summary>
		/// Gets or sets the validation message.
		/// </summary>
		/// <value>
		/// The validation message.
		/// </value>
		public string ValidationMessage { get; set; }

		/// <summary>
		/// Gets or sets the validatable object.
		/// </summary>
		/// <value>
		/// The validatable object.
		/// </value>
		public T ValidatableObject { get; set; }

		/// <summary>
		/// Checks the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>A boolean value</returns>
		public bool Check(T value)
		{
			bool result = false;

			if (value != null && ValidatableObject != null)
				result = !ValidatableObject.Equals(value);

			return result;
		}
	}
}
