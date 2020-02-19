using HopeNope.Classes;
using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// CalculatorViewModel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.BaseViewModel" />
	public class CalculatorViewModel : BaseViewModel
	{
		private const string currentAgeKey = "currentAge";
		private const int threshold = 16;
		private const int legalThreshold = 18;

		private string firstAge;
		private string secondAge;

		/// <summary>
		/// Gets a value indicating whether this instance has default age.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has default age; otherwise, <c>false</c>.
		/// </value>
		public bool HasDefaultAge
		{
			get
			{
				return Settings.HasDefaultAge;
			}
		}

		/// <summary>
		/// Gets the current age.
		/// </summary>
		/// <value>
		/// The current age.
		/// </value>
		public int CurrentAge
		{
			get
			{
				return Preferences.Get(currentAgeKey, 0);
			}
			private set
			{
				Preferences.Set(currentAgeKey, value);
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the first age.
		/// </summary>
		/// <value>
		/// The first age.
		/// </value>
		public string FirstAge
		{
			get
			{
				return firstAge;
			}
			set
			{
				firstAge = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the second age.
		/// </summary>
		/// <value>
		/// The second age.
		/// </value>
		public string SecondAge
		{
			get
			{
				return secondAge;
			}
			set
			{
				secondAge = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// The calculate command
		/// </summary>
		public ICommand CalculateCommand => new Command(DetermineHopeOrNope, CanExecuteCommands);

		/// <summary>
		/// Determines the hope or nope.
		/// </summary>
		/// <param name="obj">The object.</param>
		/// <exception cref="NotImplementedException"></exception>
		private async void DetermineHopeOrNope()
		{
			if (!FirstAge.IsNullOrWhiteSpace() && !SecondAge.IsNullOrWhiteSpace())
			{
				double firstAge = Convert.ToDouble(FirstAge);

				if (CurrentAge == 0)
					CurrentAge = (int)Math.Ceiling(firstAge);

				double secondAge = Convert.ToDouble(SecondAge);

				double calcA = firstAge > secondAge ? firstAge : secondAge;
				double calcB = firstAge > secondAge ? secondAge : firstAge;

				double minimum = Math.Ceiling((calcA / 2.0) + 7.0);

				string result = string.Empty;

				if (calcA >= threshold && calcB >= threshold)
				{
					if (minimum <= calcB)
						result = "Yes you can! There is Hope for you two!";
					else
						result = "Nope! This could be frowned upon.";
				}
				else
					result = $"Oh hell no! {threshold} should be the minimum age!";

				await AlertHandler.DisplayAlertAsync("Your results", result, "OK");

				if (await AlertHandler.DisplayAlertAsync("Reset?", "Want to try again?", "OK", "Cancel"))
					SecondAge = string.Empty;
			}
		}
	}
}
