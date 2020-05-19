namespace GuidFramework.Interfaces
{
	/// <summary>
	/// IListViewModel interface for list-based viewmodels
	/// </summary>
	public interface IListViewModel
	{
		/// <summary>
		/// Gets a value indicating whether this instance has items.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has items; otherwise, <c>false</c>.
		/// </value>
		bool HasItems { get; }
	}
}
