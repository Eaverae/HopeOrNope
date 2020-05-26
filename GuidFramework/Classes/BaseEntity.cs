namespace GuidFramework.Classes
{
	/// <summary>
	/// BaseEntity
	/// </summary>
	public class BaseEntity : NotifyPropertyChanged
	{
		private string id;

		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public string Id
		{
			get
			{
				return id;
			}
			set
			{
				id = value;
				OnPropertyChanged(nameof(id));
			}
		}
	}
}
