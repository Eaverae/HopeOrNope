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

		public int CurrentAge
		{
			get
			{
				return Settings.GetValue<int>(currentAgeKey);
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
		
		private void ButtonCalculate_Clicked(object sender, EventArgs e)
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

				if (minimum <= calcB)
					result = $"Yes you can! {minimum} is the minimum age for your date.";
				else
					result = $"Oh hell no! {minimum} is the minimum age for your date!";

				LabelResult.Text = result;
			}
		}

		private void ButtonReset_Clicked(object sender, EventArgs e)
		{
			LabelResult.Text = string.Empty;
			EntrySecondAge.Text = string.Empty;
		}
	}
}