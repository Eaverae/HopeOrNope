using Android.Gms.Ads;
using HopeNope.Droid.Services;
using HopeNope.Services;
using Xamarin.Forms;

[assembly: Dependency(typeof(InterstitialAdService))]
namespace HopeNope.Droid.Services
{
	public class InterstitialAdService : IInterstitialAdService
	{
		public bool InterstitialAdLoaded { get; private set; }

		InterstitialAd interstitialAd;

		public InterstitialAdService()
		{
			interstitialAd = new InterstitialAd(Android.App.Application.Context);
		}

		public void LoadAd(string adId)
		{
			interstitialAd.AdUnitId = adId;

			var requestbuilder = new AdRequest.Builder();
			interstitialAd.LoadAd(requestbuilder.Build());
			InterstitialAdLoaded = interstitialAd.IsLoaded;
		}

		public void ShowAd()
		{
			if (InterstitialAdLoaded)
				interstitialAd.Show();
		}
	}
}