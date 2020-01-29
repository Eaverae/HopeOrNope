using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HopeNope.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutPage : ContentPage
	{
		public AboutPage()
		{
			InitializeComponent();
			LabelCopyright.Text = String.Format(Properties.Resources.Copyright, DateTime.Now.Year);
			LabelAbout.Text = Properties.Resources.About;
		}

		private async void ButtonBack_Clicked(object sender, EventArgs e)
		{
			await Application.Current.MainPage.Navigation.PopToRootAsync();
		}
	}
}