namespace GuidFramework.Interfaces
{
	/// <summary>
	/// IValidationRule
	/// </summary>
	/// <typeparam name="T">Valuetype of the object to validate</typeparam>
	public interface IValidationRule<T>
	{
		/// <summary>
		/// Gets or sets the validation message.
		/// </summary>
		/// <value>
		/// The validation message.
		/// </value>
		string ValidationMessage { get; set; }

		/// <summary>
		/// Checks the specified value.
		/// </summary>
		/// <param name="value">The value.</param>
		/// <returns>A boolean value</returns>
		bool Check(T value);
	}
}
