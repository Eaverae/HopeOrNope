using System;

namespace HopeNope.ViewModels
{
	internal class FullscreenAdPopupViewModel : BaseViewModel
	{
		public string AdId
		{
			get;
			private set;
		}

		public FullscreenAdPopupViewModel(string adId)
		{
			if (adId.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(adId));

			AdId = adId;
		}

		public override async void BackAsync()
		{
			await GuidApp.Current.MainPage.Navigation.PopModalAsync();
		}
	}
}