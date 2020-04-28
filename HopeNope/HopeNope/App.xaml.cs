using Autofac;
using GuidFramework;
using GuidFramework.Classes;
using GuidFramework.Services;
using HopeNope.Classes;
using HopeNope.Handlers;
using HopeNope.Interfaces;
using HopeNope.ViewModels;
using HopeNope.Views;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using Xamarin.Essentials;
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
			VersionTracking.Track();

			// Start appcenter
			AppCenter.Start(ApplicationConstants.AppCenterKey, typeof(Analytics), typeof(Crashes));

			base.OnStart();

			// Set the language
			using (ILifetimeScope scope = Container.BeginLifetimeScope())
			{
				// Set the app language
				ILanguageHandler languageHandler = scope.Resolve<ILanguageHandler>();
				languageHandler.SetLanguage(languageHandler.GetUserLanguage().CultureName);
			}

			// Hide statusbar before navigating
			if (statusBarService == null)
				statusBarService = DependencyService.Get<IStatusBarService>();

			statusBarService.HideStatusBar();

			MainPage = new NavigationPage(ViewFactory.CreateView<MainViewModel>());
		}

		/// <summary>
		/// Registers the dependencies.
		/// </summary>
		/// <param name="ignoreDefaults">if set to <c>true</c> [ignores default dependencies].</param>
		protected override void RegisterDependencies(bool ignoreDefaults = false)
		{
			base.RegisterDependencies(ignoreDefaults);

			ContainerBuilder.RegisterType<LanguageHandler>().As<ILanguageHandler>();
		}

		/// <summary>
		/// Registers the views.
		/// </summary>
		protected override void RegisterViews()
		{
			base.RegisterViews();

			// Generic application views
			ViewFactory.RegisterView<MainView, MainViewModel>();
			ViewFactory.RegisterView<AboutView, AboutViewModel>();
			ViewFactory.RegisterView<StatsView, StatsViewModel>();
			ViewFactory.RegisterView<CalculatorView, CalculatorViewModel>();
			ViewFactory.RegisterView<SettingsView, SettingsViewModel>();
		}
	}
}
