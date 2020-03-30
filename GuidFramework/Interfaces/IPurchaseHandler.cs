using System.Threading.Tasks;

namespace GuidFramework.Interfaces
{
	/// <summary>
	/// PurchaseHandler interface
	/// </summary>
	public interface IPurchaseHandler
	{
		/// <summary>
		/// Makes the purchase.
		/// </summary>
		/// <param name="productId">The product Id</param>
		/// <param name="payload">The payload</param>
		/// <returns>A boolean value</returns>
		Task<bool> MakePurchase(string productId, string payload);

		/// <summary>
		/// Was the item purchased.
		/// </summary>
		/// <param name="productId">The product Id</param>
		/// <returns>A boolean value</returns>
		Task<bool> WasItemPurchased(string productId);
	}
}
