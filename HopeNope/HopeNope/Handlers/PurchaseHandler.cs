using HopeNope.Interfaces;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HopeNope.Handlers
{
	/// <summary>
	/// Purchase handler which handles all in-app-purchases
	/// </summary>
	/// <seealso cref="HopeNope.Interfaces.IPurchaseHandler" />
	public class PurchaseHandler : IPurchaseHandler
	{
		private readonly ILogHandler logHandler;
		private readonly IAlertHandler alertHandler;

		/// <summary>
		/// Gets the product identifier.
		/// </summary>
		/// <value>
		/// The product identifier.
		/// </value>
		public string ProductId
		{
			get
			{
				// ios shared secret: 0dff0c71e22841c39b17274249841999
				string returnValue = "hopenoperemoveads";

				/*if (Device.RuntimePlatform == Device.Android)
					returnValue = "hopenoperemoveads";
				else if (Device.RuntimePlatform == Device.iOS)
					returnValue = "1497520749";*/

				return returnValue;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="PurchaseHandler"/> class.
		/// </summary>
		/// <param name="logHandler">The log handler.</param>
		/// <param name="alertHandler">The alert handler</param>
		public PurchaseHandler(ILogHandler logHandler, IAlertHandler alertHandler)
		{
			this.logHandler = logHandler;
			this.alertHandler = alertHandler;
		}

		/// <summary>
		/// Makes the purchase.
		/// </summary>
		/// <returns>A boolean value</returns>
		public async Task<bool> MakePurchase()
		{
			bool returnValue = false;

			if (CrossInAppBilling.IsSupported)
			{
				IInAppBilling billing = CrossInAppBilling.Current;

				try
				{
					bool connected = await billing.ConnectAsync(ItemType.InAppPurchase);

					// Verify the connection to the stores and the product id
					if (!ProductId.IsNullOrWhiteSpace() && connected)
					{
						var product = await billing.GetProductInfoAsync(ItemType.InAppPurchase, ProductId);
						bool makepurchase = product != null;

						if (!makepurchase)
							makepurchase = await alertHandler.DisplayAlertAsync("Product is null!", $"Product {ProductId} is null! Do you still want to continue?", "Yes", "No");

						if (makepurchase)
						{
							// Make additional billing calls
							InAppBillingPurchase result = await billing.PurchaseAsync(ProductId, ItemType.InAppPurchase, payload: "test");

							if (result != null && result.State == PurchaseState.Purchased)
								returnValue = true;
							else
								returnValue = false;
						}
					}
					else
						await alertHandler.DisplayAlertAsync("Not connected!", "Not connected!", "Ok");
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
		/// Wases the item purchased.
		/// </summary>
		/// <returns></returns>
		public async Task<bool> WasItemPurchased()
		{
			var billing = CrossInAppBilling.Current;
			try
			{
				var connected = await billing.ConnectAsync(ItemType.InAppPurchase);

				if (!connected)
				{
					// Couldn't connect
					return false;
				}

				//check purchases
				var purchases = await billing.GetPurchasesAsync(ItemType.InAppPurchase);

				//check for null just incase
				if (purchases?.Any(p => p.ProductId == ProductId) ?? false)
				{
					//Purchase restored
					return true;
				}
				else
				{
					//no purchases found
					return false;
				}
			}
			catch (InAppBillingPurchaseException purchaseEx)
			{
				//Billing Exception handle this based on the type
				Debug.WriteLine("Error: " + purchaseEx);
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

			return false;
		}

	}
}
