using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Foundation;
using GuidFramework.iOS.Renderers;
using GuidFramework.iOS.Services;
using UIKit;

namespace GuidFramework.iOS
{
	/// <summary>
	/// Static framework class for iOS
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
				AdBannerRenderer adBannerRenderer = new AdBannerRenderer();
				CustomDatePickerRenderer customDatePickerRenderer = new CustomDatePickerRenderer();
				CustomEntryRenderer customEntryRenderer = new CustomEntryRenderer();
				CustomPickerRenderer customPickerRenderer = new CustomPickerRenderer();
				CustomStackLayoutRenderer customStackLayoutRenderer = new CustomStackLayoutRenderer();
				InAppBillingVerifyService inAppBillingVerifyService = new InAppBillingVerifyService();
				StatusBarService statusBarService = new StatusBarService();
			}
		}
	}
}