using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Foundation;
using Google.MobileAds;
using HopeNope.iOS.Services;
using HopeNope.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(InterstitialAdService))]
namespace HopeNope.iOS.Services
{
	public class InterstitialAdService : IInterstitialAdService
	{
		public bool InterstitialAdLoaded { get { return interstitial != null && interstitial.IsReady; } }

		Interstitial interstitial;

		public void LoadAd(string adId)
		{
			// Renew when needed
			if (interstitial == null || interstitial.AdUnitId != adId)
				interstitial = new Interstitial(adId);

			Request request = Request.GetDefaultRequest();

			interstitial.LoadRequest(request);
		}

		public void ShowAd()
		{
			if (InterstitialAdLoaded)
			{
				var viewController = GetVisibleViewController();
				interstitial.Present(viewController);
			}
		}
		UIViewController GetVisibleViewController()
		{
			var rootController = UIApplication.SharedApplication.KeyWindow.RootViewController;

			if (rootController.PresentedViewController == null)
				return rootController;

			if (rootController.PresentedViewController is UINavigationController)
			{
				return ((UINavigationController)rootController.PresentedViewController).VisibleViewController;
			}

			if (rootController.PresentedViewController is UITabBarController)
			{
				return ((UITabBarController)rootController.PresentedViewController).SelectedViewController;
			}

			return rootController.PresentedViewController;
		}

	}
}