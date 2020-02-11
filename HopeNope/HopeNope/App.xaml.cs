using HopeNope.Services;
using MarcTron.Plugin;
using System.Diagnostics;
using Xamarin.Forms;

namespace HopeNope
{
	/// <summary>
	/// App class
	/// </summary>
	/// <seealso cref="HopeNope.GuidApp" />
	public partial class App : GuidApp
	{
		private IStatusBarService statusBarService;

		/*public static string addmobAppId = "ca-app-pub-3950359454148049~9381262238";
		public static string MainBannerAdId = "ca-app-pub-3950359454148049/6551084769";
		public static string MainTransitionAdId = "ca-app-pub-3950359454148049/3683332556";*/

		/// <summary>
		/// Gets a value indicating whether [ads enabled].
		/// </summary>
		/// <value>
		///   <c>true</c> if [ads enabled]; otherwise, <c>false</c>.
		/// </value>
		public static bool AdsEnabled
		{
			get
			{
				return !Debugger.IsAttached && CrossMTAdmob.IsSupported;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="App"/> class.
		/// </summary>
		public App()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Application developers override this method to perform actions when the application starts.
		/// </summary>
		protected override void OnStart()
		{
			base.OnStart();

			// Hide statusbar before navigating
			if (statusBarService == null)
				statusBarService = DependencyService.Get<IStatusBarService>();

			statusBarService.HideStatusBar();

			MainPage = new NavigationPage(new MainPage());
		}
	}
}
