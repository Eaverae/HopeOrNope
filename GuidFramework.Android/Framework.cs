using GuidFramework.Droid.Renderers;
using GuidFramework.Droid.Services;

namespace GuidFramework.Android
{
	/// <summary>
	/// Static framework class for Android
	/// </summary>
	public static class Framework
	{
		/// <summary>
		/// Initializes the GuidFramework.
		/// </summary>
		public static void Init()
		{
			LinkAssemblies();
		}

		/// <summary>
		/// Links the assemblies.
		/// </summary>
		private static void LinkAssemblies()
		{
			// Trick to link assemblies; this is never executed.
			if (false.Equals(true))
			{
				AdBannerRenderer adBannerRenderer = new AdBannerRenderer(null);
				CustomDatePickerRenderer customDatePickerRenderer = new CustomDatePickerRenderer(null);
				CustomEntryRenderer customEntryRenderer = new CustomEntryRenderer(null);
				CustomPickerRenderer customPickerRenderer = new CustomPickerRenderer(null);
				CustomStackLayoutRenderer customStackLayoutRenderer = new CustomStackLayoutRenderer(null);
				InAppBillingVerifyService inAppBillingVerifyService = new InAppBillingVerifyService();
				StatusBarService statusBarService = new StatusBarService();
			}
		}
	}
}