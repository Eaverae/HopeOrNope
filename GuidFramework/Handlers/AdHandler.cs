using GuidFramework.Classes;
using GuidFramework.Extensions;
using GuidFramework.ViewModels;
using System;
using Xamarin.Forms;

namespace GuidFramework.Handlers
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
		/// <param name="loadingText">[Optional] The loading label text</param>
		/// <param name="continueText">[Optional] The continue label text</param>
		/// <param name="secondaryAdId">[Optional] The seondary ad identifier</param>
		/// <param name="continueWithAction">[Optional] The continue with action.</param>
		public static async void ShowFullScreenAd(string adId, string loadingText = "Loading", string continueText = "Continue", string secondaryAdId = null, Action continueWithAction = null)
		{
			if (adId.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(adId));
			
			try
			{
				Page adview = ViewFactory.CreateView(new FullscreenAdPopupViewModel(adId, loadingText, continueText, secondaryAdId));

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
