using GuidFramework.ViewModels;

namespace GuidFramework.Classes
{
	/// <summary>
	/// BaseEntity
	/// </summary>
	public class BaseEntity : NotifyPropertyChanged
	{
		/// <summary>
		/// Gets or sets the identifier.
		/// </summary>
		/// <value>
		/// The identifier.
		/// </value>
		public string Id
		{
			get;
			set;
		}
	}
}
