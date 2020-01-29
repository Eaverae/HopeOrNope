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

			LabelCopyright.Text = String.Format(Properties.Resources.Copyright, DateTime.Now.Year);

			// TODO make setting
			CrossMTAdmob.Current.UserPersonalizedAds = true;
			CrossMTAdmob.Current.OnInterstitialClosed += Current_OnInterstitialClosed;
			CrossMTAdmob.Current.OnInterstitialLoaded += Current_OnInterstitialLoaded;
		}

		private async void StartButton_Clicked(object sender, EventArgs e)
		{
			bool exceptionOccurred = false;
			if (App.AdsEnabled)
			{
				try
				{
					CrossMTAdmob.Current.LoadInterstitial(App.MainTransitionAdId);
					exceptionOccurred = !CrossMTAdmob.Current.IsInterstitialLoaded();
				}
				catch (Exception ex)
				{
					string temp = ex.ToString();
					exceptionOccurred = true;
				}
			}

			if (!App.AdsEnabled || exceptionOccurred)
				await Application.Current.MainPage.Navigation.PushAsync(new CalculatorPage());
		}

		private void Current_OnInterstitialLoaded(object sender, EventArgs e)
		{
			CrossMTAdmob.Current.ShowInterstitial();
		}

		private void Current_OnInterstitialClosed(object sender, EventArgs e)
		{
			Application.Current.MainPage.Navigation.PushAsync(new CalculatorPage());
		}

		private async void AboutButton_Clicked(object sender, EventArgs e)
		{
			await Application.Current.MainPage.Navigation.PushAsync(new AboutPage());
		}
	}
}
