using System.Threading.Tasks;

namespace HopeNope.Interfaces
{
	/// <summary>
	/// PurchaseHandler interface
	/// </summary>
	public interface IPurchaseHandler
	{
		/// <summary>
		/// Gets the product identifier.
		/// </summary>
		/// <value>
		/// The product identifier.
		/// </value>
		string ProductId { get; }

		/// <summary>
		/// Makes the purchase.
		/// </summary>
		/// <returns></returns>
		Task<bool> MakePurchase();

		/// <summary>
		/// Wases the item purchased.
		/// </summary>
		/// <returns></returns>
		Task<bool> WasItemPurchased();
	}
}
