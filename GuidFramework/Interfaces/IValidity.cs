using System.Collections.Generic;

namespace GuidFramework.Interfaces
{
	/// <summary>
	/// IValidity interface
	/// </summary>
	public interface IValidity
	{
		/// <summary>
		/// Gets the index of the validation.
		/// </summary>
		/// <value>
		/// The index of the validation.
		/// </value>
		int? ValidationIndex { get; }

		/// <summary>
		/// Returns true if this instance is valid.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
		/// </value>
		bool IsValid { get; set; }

		/// <summary>
		/// Gets the validation errors.
		/// </summary>
		/// <value>
		/// The validation errors.
		/// </value>
		List<string> ValidationErrors { get; }

		/// <summary>
		/// Validates this instance.
		/// </summary>
		void Validate();
	}
}
