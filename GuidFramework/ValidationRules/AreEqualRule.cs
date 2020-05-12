using GuidFramework.Interfaces;
using System;

namespace GuidFramework.ValidationRules
{
	/// <summary>
	/// AreEqualRule
	/// </summary>
	/// <typeparam name="T"></typeparam>
	/// <seealso cref="GuidFramework.Interfaces.IValidationRule{T}" />
	public class AreEqualRule<T> : IValidationRule<T>
	{
		/// <summary>
		/// Gets or sets the validation message.
		/// </summary>
		/// <value>
		/// The validation message.
		/// </value>
		public string ValidationMessage { get; set; }

		/// <summary>
		/// Gets or sets the validatable object function.
		/// </summary>
		/// <value>
		/// The validatable object function.
		/// </value>
		public Func<T> ValidatableObjectFunction { get; set; }

		/// <summary>
		/// Checks the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>A boolean value</returns>
		public bool Check(T value)
		{
			bool result = false;

			if (ValidatableObjectFunction != null)
			{
				T compare = ValidatableObjectFunction.Invoke();

				if (value != null && compare != null)
					result = compare.Equals(value);
			}

			return result;
		}
	}
}
