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
		Interstitial interstitial;

		public InterstitialAdService()
		{
			// LoadAd();
			// interstitial.ScreenDismissed += (s, e) => LoadAd();
		}
		public void LoadAd(string adId)
		{
			// TODO: change this id to your admob id    
			interstitial = new Interstitial(adId);

			var request = Request.GetDefaultRequest();
			// request.TestDevices = new string[] { "Your Test Device ID", "GADSimulator" };
			interstitial.LoadRequest(request);
		}

		public void ShowAd()
		{
			if (interstitial.IsReady)
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