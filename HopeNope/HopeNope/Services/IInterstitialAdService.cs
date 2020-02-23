namespace HopeNope.Services
{
	/// <summary>
	/// InterstitialAdService interface
	/// </summary>
	public interface IInterstitialAdService
	{
		/// <summary>
		/// Gets a value indicating whether [interstitial ad loaded].
		/// </summary>
		/// <value>
		///   <c>true</c> if [interstitial ad loaded]; otherwise, <c>false</c>.
		/// </value>
		bool InterstitialAdLoaded { get; }

		/// <summary>
		/// Loads the ad.
		/// </summary>
		/// <param name="adId">The ad identifier.</param>
		void LoadAd(string adId);

		/// <summary>
		/// Shows the ad.
		/// </summary>
		void ShowAd();
	}
}
