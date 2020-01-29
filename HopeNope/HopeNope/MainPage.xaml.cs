using HopeNope.Pages;
using HopeNope.Services;
using MarcTron.Plugin;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace HopeNope
{
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		public MainPage()
		{
			InitializeComponent();

			// TODO make setting
			CrossMTAdmob.Current.UserPersonalizedAds = true;
			CrossMTAdmob.Current.OnInterstitialClosed += Current_OnInterstitialClosed;
			CrossMTAdmob.Current.OnInterstitialLoaded += Current_OnInterstitialLoaded;
		}

		private void StartButton_Clicked(object sender, EventArgs e)
		{
			bool exceptionOccurred = false;

			try
			{
				CrossMTAdmob.Current.LoadInterstitial(App.MainTransitionAdId);
			}
			catch (Exception ex)
			{
				string temp = ex.ToString();
				exceptionOccurred = true;
			}

			if (!App.AdsEnabled || exceptionOccurred)
				Application.Current.MainPage.Navigation.PushAsync(new CalculatorPage());
		}

		private void Current_OnInterstitialLoaded(object sender, EventArgs e)
		{
			CrossMTAdmob.Current.ShowInterstitial();
		}

		private void Current_OnInterstitialClosed(object sender, EventArgs e)
		{
			Application.Current.MainPage.Navigation.PushAsync(new CalculatorPage());
		}
	}
}
