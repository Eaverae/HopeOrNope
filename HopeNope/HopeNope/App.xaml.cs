using HopeNope.Services;
using Xamarin.Forms;

namespace HopeNope
{
	public partial class App : Application
	{
		string addmodAppId = "ca-app-pub-3950359454148049~9381262238";
		string mainBannerAdId = "ca-app-pub-3950359454148049/6551084769";
		string mainTransitionAdId = "ca-app-pub-3950359454148049/3683332556";

		IStatusBarService statusBarService;

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
