using HopeNope.Classes;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HopeNope.Pages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalculatorPage : ContentPage
	{
		string currentAgeKey = "currentAge";
		int threshold = 16;

		public int CurrentAge
		{
			get
			{
				return Preferences.Get(currentAgeKey, 0);
			}
			set
			{
				Preferences.Set(currentAgeKey, value);
			}
		}
		public CalculatorPage()
		{
			InitializeComponent();
		}

		private async void ButtonBack_Clicked(object sender, EventArgs e)
		{
			await Application.Current.MainPage.Navigation.PopToRootAsync();
		}

		private async void ButtonCalculate_Clicked(object sender, EventArgs e)
		{
			if (!EntryFirstAge.Text.IsNullOrWhiteSpace() && !EntrySecondAge.Text.IsNullOrWhiteSpace())
			{
				int firstAge = Convert.ToInt32(EntryFirstAge.Text);

				if (CurrentAge == 0)
					CurrentAge = firstAge;

				int secondAge = Convert.ToInt32(EntrySecondAge.Text);

				int calcA = firstAge > secondAge ? firstAge : secondAge;
				int calcB = firstAge > secondAge ? secondAge : firstAge;

				int minimum = (calcA / 2) + 7;

				string result = string.Empty;

				if (calcA >= threshold && calcB >= threshold)
				{
					if (minimum <= calcB)
						result = $"Yes you can! {minimum} is the minimum age for your date.";
					else
						result = $"This could be iffy..";
				}
				else
					result = $"Oh hell no! 16 should be the minimum age!";

				await DisplayAlert("Your results", result, "OK");
			}
		}

		private void ButtonReset_Clicked(object sender, EventArgs e)
		{
			EntrySecondAge.Text = string.Empty;
		}
	}
}