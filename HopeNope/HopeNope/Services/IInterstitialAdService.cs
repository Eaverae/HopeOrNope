using System;
using System.Collections.Generic;
using System.Text;

namespace HopeNope.Services
{
	public interface IInterstitialAdService
	{
		void LoadAd(string adId);
		void ShowAd();
	}
}
