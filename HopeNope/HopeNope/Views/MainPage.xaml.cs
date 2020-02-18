using HopeNope.Handlers;
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
		public MainPage()
		{
			InitializeComponent();

			BindingContext = new MainViewModel();
		}


		private async void AboutButton_Clicked(object sender, EventArgs e)
		{
			await Application.Current.MainPage.Navigation.PushAsync(new AboutView());
		}
	}
}
