using System;

namespace HopeNope.ViewModels
{
	internal class FullscreenAdPopupViewModel
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
	}
}