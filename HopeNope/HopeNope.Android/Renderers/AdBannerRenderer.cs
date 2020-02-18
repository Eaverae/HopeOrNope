using Android.Content;
using Android.Gms.Ads;
using HopeNope.Controls;
using HopeNope.Droid.Renderers;
using HopeNope.Enums;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(AdBanner), typeof(AdBannerRenderer))]
namespace HopeNope.Droid.Renderers
{
	public class AdBannerRenderer : ViewRenderer
	{
		Context context;
		public AdBannerRenderer(Context _context) : base(_context)
		{
			context = _context;
		}

		protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
		{
			base.OnElementChanged(e);
			if (e.OldElement == null)
			{
				var banner = (Element as AdBanner);

				var adView = new AdView(Context);
				switch (banner.Size)
				{
					case AdBannerSizes.Standardbanner:
						adView.AdSize = AdSize.Banner;
						break;
					case AdBannerSizes.LargeBanner:
						adView.AdSize = AdSize.LargeBanner;
						break;
					case AdBannerSizes.MediumRectangle:
						adView.AdSize = AdSize.MediumRectangle;
						break;
					case AdBannerSizes.FullBanner:
						adView.AdSize = AdSize.FullBanner;
						break;
					case AdBannerSizes.Leaderboard:
						adView.AdSize = AdSize.Leaderboard;
						break;
					case AdBannerSizes.SmartBannerPortrait:
						adView.AdSize = AdSize.SmartBanner;
						break;
					case AdBannerSizes.Fluid:
						adView.AdSize = AdSize.Fluid;
						break;
					case AdBannerSizes.WideSkyscraper:
						adView.AdSize = AdSize.WideSkyscraper;
						break;
					case AdBannerSizes.Zzaao:
						adView.AdSize = AdSize.Zzaao;
						break;
					default:
						adView.AdSize = AdSize.Banner;
						break;
				}
				// TODO: change this id to your admob id  
				adView.AdUnitId = banner.AdId;
				var requestbuilder = new AdRequest.Builder();
				adView.LoadAd(requestbuilder.Build());
				SetNativeControl(adView);
			}
		}
	}
}