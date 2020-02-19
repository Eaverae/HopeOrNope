using HopeNope.Interfaces;
using Plugin.InAppBilling;
using Plugin.InAppBilling.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HopeNope.Handlers
{
	public class PurchaseHandler
	{
		public IAlertHandler AlertHandler;

		public string ProductId
		{
			get
			{
				string returnValue = string.Empty;

				if (Device.RuntimePlatform == Device.Android)
					returnValue = "nl20200131adhereremoveads";
				else if (Device.RuntimePlatform == Device.iOS)
					returnValue = "1497520749";

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

						if (!makepurchase)
							makepurchase = await AlertHandler.DisplayAlertAsync("Product is null!", $"Product {ProductId} is null! Do you still want to continue?", "Yes", "No");

						if (makepurchase)
						{
							// Make additional billing calls
							InAppBillingPurchase result = await billing.PurchaseAsync(ProductId, ItemType.InAppPurchase, "test", null);

							if (result != null && result.State == PurchaseState.Purchased)
								await AlertHandler.DisplayAlertAsync("Purchased!", "Purchased!", "Ok");
							else
								await AlertHandler.DisplayAlertAsync("Not purchased!", Enum.GetName(typeof(PurchaseState), result.State), "Ok");
						}
					}
					else
						await AlertHandler.DisplayAlertAsync("Not connected!", "Not connected!", "Ok");
				}
				catch (Exception ex)
				{
					string temp = ex.ToString();

					await AlertHandler.DisplayAlertAsync("Exception occurred", temp, "Ok");
				}
				finally
				{
					await billing.DisconnectAsync();
				}
			}

			return returnValue;
		}
	}
}
