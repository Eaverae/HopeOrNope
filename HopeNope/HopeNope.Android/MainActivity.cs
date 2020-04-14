using Android.App;
using Android.Content.PM;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using GuidFramework.Droid;

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
	}
}