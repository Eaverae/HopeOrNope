using Android.App;
using Android.Support.V7.App;

namespace HopeNope.Droid
{
	[Activity(Label = "HopeNope", Icon = "@mipmap/ic_launcher", Theme = "@style/splashscreen", MainLauncher = true, NoHistory = true)]
	public class SplashActivity : AppCompatActivity
	{
		protected override void OnResume()
		{
			base.OnResume();
			StartActivity(typeof(MainActivity));
		}
	}
}