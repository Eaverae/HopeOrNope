using Android.Gms.Ads;
using HopeNope.Droid.Services;
using HopeNope.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(InterstitialAdService))]
namespace HopeNope.Droid.Services
{
	public class InterstitialAdService : IInterstitialAdService
	{
		public bool InterstitialAdLoaded { get { return interstitialAd != null && interstitialAd.IsLoaded; } }

		InterstitialAd interstitialAd;

		public InterstitialAdService()
		{
			interstitialAd = new InterstitialAd(Android.App.Application.Context);
		}

		public void LoadAd(string adId)
		{
			if (interstitialAd.AdUnitId.IsNullOrWhiteSpace())
				interstitialAd.AdUnitId = adId;
			else if (interstitialAd.AdUnitId != adId)
			{
				// Renew the interstitial ad
				interstitialAd.Dispose();
				interstitialAd = new InterstitialAd(Android.App.Application.Context);
				interstitialAd.AdUnitId = adId;
			}

			AdRequest.Builder requestbuilder = new AdRequest.Builder();
			interstitialAd.LoadAd(requestbuilder.Build());
		}

		public void ShowAd()
		{
			if (InterstitialAdLoaded)
				interstitialAd.Show();
		}
	}
}