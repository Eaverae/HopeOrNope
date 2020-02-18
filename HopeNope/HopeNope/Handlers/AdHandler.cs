using HopeNope.ViewModels;
using HopeNope.Views;
using MarcTron.Plugin;
using System;

namespace HopeNope.Handlers
{
	/// <summary>
	/// AdHandler
	/// </summary>
	public static class AdHandler
	{
		/// <summary>
		/// Shows the full screen ad.
		/// </summary>
		/// <param name="adId">The ad identifier.</param>
		/// <param name="continueWithAction">The continue with action.</param>
		public static async void ShowFullScreenAd(string adId, Action continueWithAction = null)
		{
			if (adId.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(adId));

			if (CrossMTAdmob.IsSupported)
			{
				/*CrossMTAdmob.Current.OnInterstitialLoaded -= Current_OnInterstitialLoaded;
				CrossMTAdmob.Current.OnInterstitialLoaded += Current_OnInterstitialLoaded;

				CrossMTAdmob.Current.OnInterstitialClosed += (s, e) =>
				{
					// Execute the given action
					if (continueWithAction != null)
						continueWithAction.Invoke();
				};*/

				try
				{
					// CrossMTAdmob.Current.LoadInterstitial(adId);

					FullscreenAdPopup adview = new FullscreenAdPopup()
					{
						BindingContext = new FullscreenAdPopupViewModel(adId)
					};

					await GuidApp.Current.MainPage.Navigation.PushModalAsync(adview);

					// Execute the given action
					if (continueWithAction != null)
						continueWithAction.Invoke();
				}
				catch
				{
					// Execute the given action
					if (continueWithAction != null)
						continueWithAction.Invoke();
				}
			}
		}

		private static void Current_OnInterstitialLoaded(object sender, EventArgs e)
		{
			CrossMTAdmob.Current.ShowInterstitial();
		}
	}
}
