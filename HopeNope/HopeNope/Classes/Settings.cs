using Xamarin.Essentials;

namespace HopeNope.Classes
{
	/// <summary>
	/// Settings class which holds all the settings for the app
	/// </summary>
	internal static class Settings
	{
		private const string adsEnabledKey = "adsEnabled";
		private const string hasDefaultAgeKey = "hasDefaultAge";
		private const string personalizedAdsKey = "personalizedAds";

		/// <summary>
		/// Gets or sets a value indicating whether [ads enabled].
		/// </summary>
		/// <value>
		///   <c>true</c> if [ads enabled]; otherwise, <c>false</c>.
		/// </value>
		internal static bool AdsEnabled
		{
			get
			{
				return Preferences.Get(adsEnabledKey, true);
			}
			set
			{
				Preferences.Set(adsEnabledKey, value);
			}
		}
		/// <summary>
		/// Gets or sets a value indicating whether this instance has default age.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has default age; otherwise, <c>false</c>.
		/// </value>
		internal static bool HasDefaultAge
		{
			get
			{
				return Preferences.Get(hasDefaultAgeKey, false);
			}
			set
			{
				Preferences.Set(hasDefaultAgeKey, value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [user personalized ads].
		/// </summary>
		/// <value>
		///   <c>true</c> if [user personalized ads]; otherwise, <c>false</c>.
		/// </value>
		internal static bool UserPersonalizedAds
		{
			get
			{
				return Preferences.Get(personalizedAdsKey, false);
			}
			set
			{
				Preferences.Set(personalizedAdsKey, value);
			}
		}
	}
}
