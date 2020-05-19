using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using GuidFramework.Android;
using GuidFramework.Droid;
using System;
using System.Threading.Tasks;

namespace HopeNope.Droid
{
	/// <summary>
	/// MainActivity
	/// </summary>
	/// <seealso cref="GuidFramework.Droid.GuidFrameworkActivity" />
	[Activity(Label = "HopeNope", Theme = "@style/MainTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : GuidFrameworkActivity
	{
		/// <summary>
		/// Called when the activity is created.
		/// </summary>
		/// <param name="bundle">The bundle.</param>
		protected override void OnCreate(Bundle bundle)
		{
			TabLayoutResource = Resource.Layout.Tabbar;
			ToolbarResource = Resource.Layout.Toolbar;

			base.OnCreate(bundle);

			MobileAds.Initialize(ApplicationContext, "ca-app-pub-3950359454148049~9381262238");

			AppDomain.CurrentDomain.UnhandledException += OnCurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException += OnTaskSchedulerOnUnobservedTaskException;
			AndroidEnvironment.UnhandledExceptionRaiser += OnAndroidEnvironmentUnhandledException;
			Java.Lang.Thread.DefaultUncaughtExceptionHandler = new UncaughtExceptionHandler();

			global::Xamarin.Forms.Forms.Init(this, bundle);

			Framework.Init();

			LoadApplication(new App());
		}

		/// <summary>
		/// OnRequestPermissionsResult.
		/// </summary>
		/// <param name="requestCode">To be added.</param>
		/// <param name="permissions">To be added.</param>
		/// <param name="grantResults">To be added.</param>
		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
		{
			Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

			base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}

		/// <summary>
		/// Override of the dispose method
		/// </summary>
		/// <param name="disposing">Disposing</param>
		protected override void Dispose(bool disposing)
		{
			AppDomain.CurrentDomain.UnhandledException -= OnCurrentDomainOnUnhandledException;
			TaskScheduler.UnobservedTaskException -= OnTaskSchedulerOnUnobservedTaskException;
			AndroidEnvironment.UnhandledExceptionRaiser -= OnAndroidEnvironmentUnhandledException;

			base.Dispose(disposing);
		}

		/// <summary>
		/// Called when [android environment unhandled exception].
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="RaiseThrowableEventArgs"/> instance containing the event data.</param>
		private static void OnAndroidEnvironmentUnhandledException(object sender, RaiseThrowableEventArgs e)
		{
			LogUnhandledException(e.Exception);
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
		/// <param name="eventArguments">The <see cref="UnhandledExceptionEventArgs"/> instance containing the event data.</param>
		private static void OnCurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs eventArguments)
		{
			LogUnhandledException(eventArguments.ExceptionObject as Exception);
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