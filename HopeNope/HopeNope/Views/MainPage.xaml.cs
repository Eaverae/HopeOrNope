using HopeNope.ViewModels;
using HopeNope.Views;
using MarcTron.Plugin;
using System;
using System.ComponentModel;
using Xamarin.Forms;

namespace HopeNope
{
	[DesignTimeVisible(false)]
	public partial class MainPage : ContentPage
	{
		MainViewModel viewModel = new MainViewModel();

		public MainPage()
		{
			InitializeComponent();

			BindingContext = viewModel;

			try
			{
				// TODO make setting
				CrossMTAdmob.Current.UserPersonalizedAds = true;
				CrossMTAdmob.Current.OnInterstitialClosed += Current_OnInterstitialClosed;
				CrossMTAdmob.Current.OnInterstitialLoaded += Current_OnInterstitialLoaded;
			}
			catch
			{ }
		}

		private async void StartButton_Clicked(object sender, EventArgs e)
		{
			bool exceptionOccurred = false;
			if (App.AdsEnabled)
			{
				try
				{
					CrossMTAdmob.Current.LoadInterstitial(viewModel.MainTransitionAdId);
					exceptionOccurred = !CrossMTAdmob.Current.IsInterstitialLoaded();
				}
				catch (Exception ex)
				{
					string temp = ex.ToString();
					exceptionOccurred = true;
				}
			}
			
			if (!App.AdsEnabled || exceptionOccurred)
				await Application.Current.MainPage.Navigation.PushAsync(new CalculatorView());
		}

		private void Current_OnInterstitialLoaded(object sender, EventArgs e)
		{
			CrossMTAdmob.Current.ShowInterstitial();
		}

		private void Current_OnInterstitialClosed(object sender, EventArgs e)
		{
			Application.Current.MainPage.Navigation.PushAsync(new CalculatorView());
		}

		private async void AboutButton_Clicked(object sender, EventArgs e)
		{
			await Application.Current.MainPage.Navigation.PushAsync(new AboutView());
		}
	}
}
