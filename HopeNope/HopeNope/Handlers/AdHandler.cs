using MarcTron.Plugin;
using System;
using System.Collections.Generic;
using System.Text;

namespace HopeNope.Handlers
{
	public static class AdHandler
	{
		public static void ShowFullScreenAd(string adId, bool continueOnClosed = false, Action continueWithAction = null)
		{
			if (CrossMTAdmob.IsSupported)
			{
				bool exceptionOccurred = false;
				bool executeGivenAction = false;

				CrossMTAdmob.Current.OnInterstitialLoaded += (s, e) =>
				{
					CrossMTAdmob.Current.ShowInterstitial();
				};

				CrossMTAdmob.Current.OnInterstitialClosed += (s, e) =>
				{
					executeGivenAction = true;
				};

				try
				{
					CrossMTAdmob.Current.LoadInterstitial(adId);
					exceptionOccurred = !CrossMTAdmob.Current.IsInterstitialLoaded();
				}
				catch (Exception ex)
				{
					string temp = ex.ToString();
					exceptionOccurred = true;
				}

				// Execute the given action
				if (exceptionOccurred || executeGivenAction && continueWithAction != null)
					continueWithAction.Invoke();
			}
		}
	}
}
