
using Android.App;
using Android.OS;
using Plugin.CurrentActivity;

namespace GuidFramework.Android
{
	[Activity(Label = "GuidFrameworkActivity")]
	public class GuidFrameworkActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
	{
		public static Activity CurrentActivity { get; private set; }

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			CurrentActivity = this;

			Rg.Plugins.Popup.Popup.Init(this, bundle);
			Xamarin.Essentials.Platform.Init(this, bundle);
			CrossCurrentActivity.Current.Init(this, bundle);
		}
	}
}