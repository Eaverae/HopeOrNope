namespace HopeNope.Entities
{
	/// <summary>
	/// Language object
	/// </summary>
	public class Language
	{
		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>
		/// The display name.
		/// </value>
		public string DisplayName { get; set; }

		/// <summary>
		/// Gets or sets the short name.
		/// </summary>
		/// <value>
		/// The short name.
		/// </value>
		public string CultureName { get; set; }

		/// <summary>
		/// Converts to string.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return DisplayName;
		}
	}
}
