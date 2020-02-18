using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using Google.MobileAds;
using HopeNope.Controls;
using HopeNope.Enums;
using HopeNope.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AdBanner), typeof(AdBannerRenderer))]
namespace HopeNope.iOS.Renderers
{
	public class AdBannerRenderer : ViewRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<View> e)
		{
			base.OnElementChanged(e);

			if (e.OldElement == null)
			{
				BannerView bannerView = null;

				var banner = (Element as AdBanner);

				switch (banner.Size)
				{
					case AdBannerSizes.Standardbanner:
						bannerView = new BannerView(AdSizeCons.Banner, new CGPoint(0, 0));
						break;
					case AdBannerSizes.LargeBanner:
						bannerView = new BannerView(AdSizeCons.LargeBanner, new CGPoint(0, 0));
						break;
					case AdBannerSizes.MediumRectangle:
						bannerView = new BannerView(AdSizeCons.MediumRectangle, new CGPoint(0, 0));
						break;
					case AdBannerSizes.FullBanner:
						bannerView = new BannerView(AdSizeCons.FullBanner, new CGPoint(0, 0));
						break;
					case AdBannerSizes.Leaderboard:
						bannerView = new BannerView(AdSizeCons.Leaderboard, new CGPoint(0, 0));
						break;
					case AdBannerSizes.SmartBannerPortrait:
						bannerView = new BannerView(AdSizeCons.SmartBannerPortrait, new CGPoint(0, 0));
						break;
					default:
						bannerView = new BannerView(AdSizeCons.Banner, new CGPoint(0, 0));
						break;
				}

				// TODO: change this id to your admob id  
				bannerView.AdUnitId = banner.AdId;

				foreach (UIWindow uiWindow in UIApplication.SharedApplication.Windows)
				{
					if (uiWindow.RootViewController != null)
					{
						bannerView.RootViewController = uiWindow.RootViewController;
					}
				}
				var request = Request.GetDefaultRequest();
				bannerView.LoadRequest(request);
				SetNativeControl(bannerView);
			}

		}
	}

}