using Android.App;
using Android.Support.V7.App;

namespace HopeNope.Droid
{
	/// <summary>
	/// SplashActivity
	/// </summary>
	/// <seealso cref="Android.Support.V7.App.AppCompatActivity" />
	[Activity(Label = "Hope or Nope", Icon = "@mipmap/ic_launcher", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : AppCompatActivity
	{
		/// <summary>
		/// Called after <see cref="M:Android.App.Activity.OnRestoreInstanceState(Android.OS.Bundle)" />, <see cref="M:Android.App.Activity.OnRestart" />, or
		/// <see cref="M:Android.App.Activity.OnPause" />, for your activity to start interacting with the user.
		/// </summary>
		/// <remarks>
		/// Portions of this page are modifications based on work created and shared by the <format type="text/html"><a href="https://developers.google.com/terms/site-policies" title="Android Open Source Project">Android Open Source Project</a></format> and used according to terms described in the <format type="text/html"><a href="https://creativecommons.org/licenses/by/2.5/" title="Creative Commons 2.5 Attribution License">Creative Commons 2.5 Attribution License.</a></format>
		/// </remarks>
		/// <since version="Added in API level 1" />
		/// <altmember cref="M:Android.App.Activity.OnRestoreInstanceState(Android.OS.Bundle)" />
		/// <altmember cref="M:Android.App.Activity.OnRestart" />
		/// <altmember cref="M:Android.App.Activity.OnPostResume" />
		/// <altmember cref="M:Android.App.Activity.OnPause" />
		protected override void OnResume()
		{
			base.OnResume();
			StartActivity(typeof(MainActivity));
		}
	}
}