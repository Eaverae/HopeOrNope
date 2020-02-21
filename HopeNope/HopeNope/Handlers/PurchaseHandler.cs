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
	public class PurchaseHandler : IPurchaseHandler
	{
		public string ProductId
		{
			get
			{
				string returnValue = string.Empty;

				if (Device.RuntimePlatform == Device.Android)
					returnValue = "hopenoperemoveads";
				//else if (Device.RuntimePlatform == Device.iOS)
				//returnValue = "1497520749";

				return returnValue;
			}
		}

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
					if (!String.IsNullOrWhiteSpace(ProductId) && connected)
					{
						var product = await billing.GetProductInfoAsync(ItemType.InAppPurchase, ProductId);
						bool makepurchase = product != null;

						// if (!makepurchase)
						// makepurchase = await AlertHandler.DisplayAlertAsync("Product is null!", $"Product {ProductId} is null! Do you still want to continue?", "Yes", "No");

						if (makepurchase)
						{
							// Make additional billing calls
							InAppBillingPurchase result = await billing.PurchaseAsync(ProductId, ItemType.InAppPurchase, payload: "test");

							if (result != null && result.State == PurchaseState.Purchased)
								returnValue = true;
							else
								returnValue = false;
							/*await AlertHandler.DisplayAlertAsync("Purchased!", "Purchased!", "Ok");
						else
							await AlertHandler.DisplayAlertAsync("Not purchased!", Enum.GetName(typeof(PurchaseState), result.State), "Ok");*/
						}
					}
					//else
					//	await AlertHandler.DisplayAlertAsync("Not connected!", "Not connected!", "Ok");
				}
				catch (Exception ex)
				{
					string temp = ex.ToString();

					//await AlertHandler.DisplayAlertAsync("Exception occurred", temp, "Ok");
				}
				finally
				{
					await billing.DisconnectAsync();
				}
			}

			return returnValue;
		}

		public async Task<bool> WasItemPurchased()
		{
			var billing = CrossInAppBilling.Current;
			try
			{
				var connected = await billing.ConnectAsync(ItemType.InAppPurchase);

				if (!connected)
				{
					//Couldn't connect
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
			catch (Exception ex)
			{
				//Something has gone wrong
			}
			finally
			{
				await billing.DisconnectAsync();
			}

			return false;
		}

	}
}
