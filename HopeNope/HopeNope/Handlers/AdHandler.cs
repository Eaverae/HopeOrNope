using HopeNope.Services;
using HopeNope.ViewModels;
using HopeNope.Views;
using System;
using Xamarin.Forms;

namespace HopeNope.Handlers
{
	/// <summary>
	/// AdHandler
	/// </summary>
	public static class AdHandler
	{
		private static IInterstitialAdService adService;

		/// <summary>
		/// Loads the interstitial ad.
		/// </summary>
		/// <param name="adId">The ad identifier.</param>
		/// <exception cref="System.ArgumentNullException">adId</exception>
		public static void LoadInterstitialAd(string adId)
		{
			if (adId.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(adId));

			if (adService == null)
				adService = DependencyService.Get<IInterstitialAdService>();

			if (!adService.InterstitialAdLoaded)
				adService.LoadAd(adId);
		}

		/// <summary>
		/// Shows the interstitial ad.
		/// </summary>
		/// <param name="continueWithAction">The continue with action.</param>
		public static void ShowInterstitialAd(Action continueWithAction = null)
		{
			if (adService == null)
				adService = DependencyService.Get<IInterstitialAdService>();

			try
			{
				if (adService.InterstitialAdLoaded)
					adService.ShowAd();

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

		/// <summary>
		/// Shows the full screen ad.
		/// </summary>
		/// <param name="adId">The ad identifier.</param>
		/// <param name="secondaryAdId">[Optional] The seondary ad identifier</param>
		/// <param name="continueWithAction">[Optional] The continue with action.</param>
		public static async void ShowFullScreenAd(string adId, string secondaryAdId = null, Action continueWithAction = null)
		{
			if (adId.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(adId));
			
			try
			{
				FullscreenAdPopup adview = new FullscreenAdPopup()
				{
					BindingContext = new FullscreenAdPopupViewModel(adId, secondaryAdId)
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
}
