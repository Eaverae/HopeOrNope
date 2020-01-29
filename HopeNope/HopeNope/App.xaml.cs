using HopeNope.Services;
using MarcTron.Plugin;
using Xamarin.Forms;

namespace HopeNope
{
	public partial class App : Application
	{
		IStatusBarService statusBarService;

		public static bool AdsEnabled
		{
			get
			{
				// return !Debugger.IsAttached; 
				return CrossMTAdmob.IsSupported;
			}
		}

		public static string addmobAppId = "ca-app-pub-3950359454148049~9381262238";
		public static string MainBannerAdId = "ca-app-pub-3950359454148049/6551084769";
		public static string MainTransitionAdId = "ca-app-pub-3950359454148049/3683332556";

		public App()
		{
			InitializeComponent();

			// Hide statusbar before navigating
			if (statusBarService == null)
				statusBarService = DependencyService.Get<IStatusBarService>();

			statusBarService.HideStatusBar();

			MainPage = new NavigationPage(new MainPage());
		}
	}
}
