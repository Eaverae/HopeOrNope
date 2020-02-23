using System;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// FullscreenAdPopupViewModel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.BaseViewModel" />
	public class FullscreenAdPopupViewModel : BaseViewModel
	{
		/// <summary>
		/// Gets the ad identifier.
		/// </summary>
		/// <value>
		/// The ad identifier.
		/// </value>
		public string AdId
		{
			get;
			private set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="FullscreenAdPopupViewModel"/> class.
		/// </summary>
		/// <param name="adId">The ad identifier.</param>
		/// <exception cref="System.ArgumentNullException">adId</exception>
		public FullscreenAdPopupViewModel(string adId)
		{
			if (adId.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(adId));

			AdId = adId;
		}

		/// <summary>
		/// Navigates the viewmodel back asynchronous.
		/// </summary>
		public override async void BackAsync()
		{
			await NavigationService.PopModalAsync();
		}
	}
}