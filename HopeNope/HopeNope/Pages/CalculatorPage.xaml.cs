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
		int legalThreshold = 18;

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
				double firstAge = Convert.ToDouble(EntryFirstAge.Text);

				if (CurrentAge == 0)
					CurrentAge = (int)Math.Ceiling(firstAge);

				double secondAge = Convert.ToDouble(EntrySecondAge.Text);

				double calcA = firstAge > secondAge ? firstAge : secondAge;
				double calcB = firstAge > secondAge ? secondAge : firstAge;

				double minimum = Math.Ceiling((calcA / 2.0) + 7.0);

				string result = string.Empty;

				if (calcA >= threshold && calcB >= threshold)
				{
					/*if (calcB == threshold && calcA > legalThreshold || calcA == threshold && calcB > legalThreshold)
						result = "This may be illegal in some countries. Better check first!";
					else */
					if (minimum <= calcB)
						result = "Yes you can! There is Hope for you two!";
					else
						result = "Nope! This could be frowned upon.";
				}
				else
					result = $"Oh hell no! 16 should be the minimum age!";

				await DisplayAlert("Your results", result, "OK");

				if (await DisplayAlert("Reset?", "Want to try again?", "OK", "Cancel"))
					EntrySecondAge.Text = string.Empty;
			}
		}
	}
}