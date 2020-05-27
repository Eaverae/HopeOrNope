using Foundation;
using Google.MobileAds;
using ImageCircle.Forms.Plugin.iOS;
using System;
using System.Threading.Tasks;
using UIKit;

namespace HopeNope.iOS
{
	/// <summary>
	/// AppDelegate
	/// </summary>
	/// <seealso cref="Xamarin.Forms.Platform.iOS.FormsApplicationDelegate" />
	[Register("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		/// <summary>
		/// Runs when the app launched.
		/// </summary>
		/// <param name="app">The application.</param>
		/// <param name="options">The options.</param>
		/// <returns></returns>
		public override bool FinishedLaunching(UIApplication app, NSDictionary options)
		{
			AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException += OnTaskSchedulerOnUnobservedTaskException;

			Rg.Plugins.Popup.Popup.Init();
			global::Xamarin.Forms.Forms.Init();

			ImageCircleRenderer.Init();
			MobileAds.SharedInstance.Start(null);
			GuidFramework.iOS.Framework.Init();
			
			LoadApplication(new App());

			return base.FinishedLaunching(app, options);
		}

		/// <summary>
		/// Releases unmanaged and - optionally - managed resources.
		/// </summary>
		/// <param name="disposing"><c>true</c> to release both managed and unmanaged resources; <c>false</c> to release only unmanaged resources.</param>
		protected override void Dispose(bool disposing)
		{
			AppDomain.CurrentDomain.UnhandledException -= OnCurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException -= OnTaskSchedulerOnUnobservedTaskException;

			base.Dispose(disposing);
		}

		/// <summary>
		/// Called when [task scheduler on unobserved task exception].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="UnobservedTaskExceptionEventArgs"/> instance containing the event data.</param>
		private static void OnTaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			LogUnhandledException(e.Exception);
		}

		/// <summary>
		/// Called when [current domain on unhandled exception].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
		private static void OnCurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			LogUnhandledException(e.ExceptionObject as Exception);
		}

		/// <summary>
		/// Logs the unhandled exception.
		/// </summary>
		/// <param name="unhandledException">The unhandled exception.</param>
		private static void LogUnhandledException(Exception unhandledException)
		{
			App.OnUnhandledException(unhandledException);
		}
	}
}
