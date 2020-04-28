using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
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
			Java.Lang.Thread.DefaultUncaughtExceptionHandler = this;

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
		/// Method that gets called on UnhandledException in AndroidEnvironment
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private static void OnAndroidEnvironmentUnhandledException(object sender, RaiseThrowableEventArgs e)
		{
			LogUnhandledException(AnalyticsResources.AndroidEnvironmentUnhandledExceptionRaiser, e.Exception);
		}

		/// <summary>
		/// Method that gets called on UnhandledException in TaskScheduler
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		private static void OnTaskSchedulerOnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
		{
			LogUnhandledException(AnalyticsResources.TaskSchedulerOnUnobservedTaskException, e.Exception);
		}

		/// <summary>
		/// Method that gets called on UnhandledException in CurrentDomain
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="eventArguments">Event arguments</param>
		private static void OnCurrentDomainOnUnhandledException(object sender, UnhandledExceptionEventArgs eventArguments)
		{
			LogUnhandledException(AnalyticsResources.CurrentDomainOnUnhandledException, eventArguments.ExceptionObject as System.Exception);
		}

		/// <summary>
		/// Logs an unhandled exception
		/// </summary>
		/// <param name="exceptionName">Name of 'type' of the exception</param>
		/// <param name="unhandledException">Unhandled exception to log</param>
		private static void LogUnhandledException(string exceptionName, System.Exception unhandledException)
		{
			App.OnUnhandledException(exceptionName, unhandledException);
		}

		public void UncaughtException(Thread t, Throwable e)
		{
			App.OnUnhandledException(e.GetType().Name, new System.Exception(e));
		}
	}
}