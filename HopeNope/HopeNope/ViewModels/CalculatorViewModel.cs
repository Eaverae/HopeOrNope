using HopeNope.Classes;
using HopeNope.Views;
using System;
using System.Collections.Generic;
using System.Linq;
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
		private const int maxAds = 3;

		private string firstAge;
		private string secondAge;
		private bool hope;
		
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
		/// Gets or sets a value indicating whether this <see cref="CalculatorViewModel"/> is hope.
		/// </summary>
		/// <value>
		///   <c>true</c> if hope; otherwise, <c>false</c>.
		/// </value>
		public bool Hope
		{
			get
			{
				return hope;
			}
			set
			{
				hope = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// The calculate command
		/// </summary>
		public ICommand CalculateCommand => new Command(DetermineHopeOrNope, CanExecuteCommands);

		/// <summary>
		/// Gets the reset command.
		/// </summary>
		/// <value>
		/// The reset command.
		/// </value>
		public ICommand ResetCommand => new Command(Reset, CanExecuteCommands);

		/// <summary>
		/// Gets the select first tab command.
		/// </summary>
		/// <value>
		/// The select first tab command.
		/// </value>
		public ICommand SelectFirstTabCommand => new Command(SelectFirstTab, CanExecuteCommands);

		/// <summary>
		/// Gets the select second tab command.
		/// </summary>
		/// <value>
		/// The select second tab command.
		/// </value>
		public ICommand SelectSecondTabCommand => new Command(SelectSecondTab, CanExecuteCommands);

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

				if (calcA >= threshold && calcB >= threshold)
				{
					if (minimum <= calcB)
						Hope = true;
					else
						Hope = false;
				}
				else
					await ToastHandler.ShowErrorMessageAsync($"Oh hell no! {threshold} should be the minimum age!");
			}

			// View the result
			SetSelectedItem<WizardPage3>();
		}

		/// <summary>
		/// Selects the first tab.
		/// </summary>
		private void SelectFirstTab()
		{
			SetSelectedItem<WizardPage1>();
		}

		/// <summary>
		/// Selects the second tab.
		/// </summary>
		private void SelectSecondTab()
		{
			SetSelectedItem<WizardPage2>();
		}

		/// <summary>
		/// Resets this instance.
		/// </summary>
		private void Reset()
		{
			SecondAge = string.Empty;

			SelectSecondTab();
		}

		/// <summary>
		/// Sets the selected item.
		/// </summary>
		/// <typeparam name="TPage">The type of the page.</typeparam>
		private static void SetSelectedItem<TPage>()
			where TPage : ContentPage
		{
			// Get a reference to the carouselpage
			CarouselPage carouselPage = Services.NavigationService.CurrentPage<CarouselPage>();

			if (carouselPage != null)
			{
				// Find the childpage
				var resultPage = carouselPage.Children.Single(page => page.GetType().Equals(typeof(TPage)));

				// Set the resultpage as the selected item
				carouselPage.SelectedItem = resultPage;
			}
		}
	}
}
