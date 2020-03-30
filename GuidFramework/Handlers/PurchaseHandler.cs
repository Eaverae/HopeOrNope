using GuidFramework.Extensions;
using GuidFramework.Interfaces;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GuidFramework.Handlers
{
	/// <summary>
	/// Purchase handler which handles all in-app-purchases
	/// </summary>
	/// <seealso cref="GuidFramework.Interfaces.IPurchaseHandler" />
	public class PurchaseHandler : IPurchaseHandler
	{
		private readonly ILogHandler logHandler;

		/// <summary>
		/// Initializes a new instance of the <see cref="PurchaseHandler"/> class.
		/// </summary>
		/// <param name="logHandler">The log handler.</param>
		public PurchaseHandler(ILogHandler logHandler)
		{
			this.logHandler = logHandler;
		}

		/// <summary>
		/// Makes the purchase.
		/// </summary>
		/// <param name="productId">The product Id</param>
		/// <param name="payload">The payload</param>
		/// <returns>
		/// A boolean value
		/// </returns>
		/// <exception cref="ArgumentNullException">productId</exception>
		public async Task<bool> MakePurchase(string productId, string payload)
		{
			if (productId.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(productId));

			if (payload.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(payload));

			bool returnValue = false;

			if (CrossInAppBilling.IsSupported)
			{
				IInAppBilling billing = CrossInAppBilling.Current;

				try
				{
					// Verify the connection to the stores and the product id
					if (await billing.ConnectAsync(ItemType.InAppPurchase))
					{
						var product = await billing.GetProductInfoAsync(ItemType.InAppPurchase, productId);

						if (product != null)
						{
							// Make additional billing calls
							InAppBillingPurchase result = await billing.PurchaseAsync(productId, ItemType.InAppPurchase, payload);

							if (result != null && result.State == PurchaseState.Purchased)
								returnValue = true;
						}
					}
				}
				catch (Exception exception)
				{
					logHandler.LogException(exception);
				}
				finally
				{
					await billing.DisconnectAsync();
				}
			}

			return returnValue;
		}

		/// <summary>
		/// Was the item purchased.
		/// </summary>
		/// <param name="productId">The product Id</param>
		/// <returns>
		/// A boolean value
		/// </returns>
		/// <exception cref="ArgumentNullException">productId</exception>
		public async Task<bool> WasItemPurchased(string productId)
		{
			if (productId.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(productId));

			bool returnValue = false;

			var billing = CrossInAppBilling.Current;
			try
			{
				if (await billing.ConnectAsync(ItemType.InAppPurchase))
				{
					// Check purchases
					var purchases = await billing.GetPurchasesAsync(ItemType.InAppPurchase);

					// Check for null just incase
					returnValue = (purchases?.Any(purchase => purchase.ProductId == productId)).Value;
				}
			}
			catch (Exception exception)
			{
				// Something has gone wrong
				logHandler.LogException(exception);
			}
			finally
			{
				await billing.DisconnectAsync();
			}

			return returnValue;
		}
	}
}
