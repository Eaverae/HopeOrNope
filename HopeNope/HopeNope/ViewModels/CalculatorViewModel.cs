using HopeNope.Classes;
using HopeNope.Handlers;
using HopeNope.Views;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// CalculatorViewModel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.BaseViewModel" />
	public class CalculatorViewModel : BaseViewModel
	{
		private const int threshold = 16;
		private const int legalThreshold = 18;
		private int maxAds = 3;

		private string firstAge;
		private string secondAge;
		private bool hope;
		private bool isWizardInitialized;

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
		/// Initializes this instance.
		/// <para>Sets IsInitialized to true</para>
		/// </summary>
		public override void Init()
		{
			if (HasDefaultAge)
				FirstAge = Settings.DefaultAge.ToString();

			base.Init();
		}

		/// <summary>
		/// Appearing method
		/// <para>This method will be called when the page appears</para>
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event arguments</param>
		public override void OnAppearing(object sender, EventArgs e)
		{
			base.OnAppearing(sender, e);
			
			// This code may only execute when the wizard first appears.
			if (HasDefaultAge && !isWizardInitialized)
			{
				Services.NavigationService.MultipageSetSelectedItem<WizardPage2>();
				isWizardInitialized = true;
			}
		}

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

			if (AdsEnabled && maxAds > 0)
			{
				AdHandler.ShowFullScreenAd(BannerAdId, SecondBannerAdId, () =>
				{
					NavigateToResult();
					maxAds--;
				});
			}
			else
				NavigateToResult();

			// Local function for navigation
			void NavigateToResult()
			{
				// View the result
				Services.NavigationService.MultipageSetSelectedItem<WizardPage3>();
			}
		}

		/// <summary>
		/// Selects the first tab.
		/// </summary>
		private void SelectFirstTab()
		{
			Services.NavigationService.MultipageSetSelectedItem<WizardPage1>();
		}

		/// <summary>
		/// Selects the second tab.
		/// </summary>
		private void SelectSecondTab()
		{
			if (AdsEnabled && maxAds > 0)
			{
				AdHandler.ShowFullScreenAd(BannerAdId, SecondBannerAdId, () =>
				{
					NavigateToSecondTab();
					maxAds--;
				});
			}
			else
				NavigateToSecondTab();

			// Local function for navigation
			void NavigateToSecondTab()
			{
				Services.NavigationService.MultipageSetSelectedItem<WizardPage2>();
			}
		}

		/// <summary>
		/// Resets this instance.
		/// </summary>
		private void Reset()
		{
			SecondAge = string.Empty;

			SelectSecondTab();
		}
	}
}
