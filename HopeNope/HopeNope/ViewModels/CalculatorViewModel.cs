using GuidFramework.Extensions;
using GuidFramework.Handlers;
using HopeNope.Classes;
using HopeNope.Entities;
using HopeNope.Handlers;
using HopeNope.Properties;
using HopeNope.ViewModels.Base;
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
	public class CalculatorViewModel : HopeNopeViewModel
	{
		private int maxAds = new Random().Next(2, 5);

		private string firstAgeInput;
		private string secondAgeInput;
		private bool hope;
		private bool isWizardInitialized;

		/// <summary>
		/// Gets the first age.
		/// </summary>
		/// <value>
		/// The first age.
		/// </value>
		private double FirstAge
		{
			get
			{
				double firstAge;

				if (!double.TryParse(firstAgeInput, out firstAge))
					firstAge = 0;

				return firstAge;
			}
		}

		/// <summary>
		/// Gets the second age.
		/// </summary>
		/// <value>
		/// The second age.
		/// </value>
		private double SecondAge
		{
			get
			{
				double secondAge;

				if (!double.TryParse(secondAgeInput, out secondAge))
					secondAge = 0;

				return secondAge;
			}
		}

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
		public string FirstAgeInput
		{
			get
			{
				return firstAgeInput;
			}
			set
			{
				firstAgeInput = value;
				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets or sets the second age.
		/// </summary>
		/// <value>
		/// The second age.
		/// </value>
		public string SecondAgeInput
		{
			get
			{
				return secondAgeInput;
			}
			set
			{
				secondAgeInput = value;
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
		/// Gets the statistics command.
		/// </summary>
		/// <value>
		/// The statistics command.
		/// </value>
		public ICommand StatisticsCommand => new Command(async () =>
		{
			await NavigationService.NavigateAsync<StatsViewModel>(animated: false);
		});

		/// <summary>
		/// Initializes this instance.
		/// <para>Sets IsInitialized to true</para>
		/// </summary>
		public override void Init()
		{
			if (HasDefaultAge)
				FirstAgeInput = Settings.DefaultAge.ToString();

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
				GuidFramework.Services.NavigationService.MultipageSetSelectedItem<WizardPage2>();
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
			if (SecondAgeInput.IsNullOrWhiteSpace())
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageInputAgeEmpty);
			else if (SecondAge <= 0)
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageInputAgeInvalidInput);
			else if (SecondAge < Settings.MinimumAgeThreshold)
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageAgeTooLow);
			else
			{
				double calcA = FirstAge > SecondAge ? FirstAge : SecondAge;
				double calcB = FirstAge > SecondAge ? SecondAge : FirstAge;

				double minimum = Math.Ceiling((calcA / 2.0) + 7.0);

				if (minimum <= calcB)
					Hope = true;
				else
					Hope = false;

				// Add the result as a statistic
				Settings.SaveStatistic(new CalculatedResult()
				{
					Age = calcA,
					CompareAge = calcB,
					DeterminedDate = DateTime.Now,
					Verdict = Hope
				});

				if (AdsEnabled && maxAds > 0)
				{
					AdHandler.ShowFullScreenAd(BannerAdId, Resources.Loading, Resources.Continue, SecondBannerAdId, () =>
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
					GuidFramework.Services.NavigationService.MultipageSetSelectedItem<WizardPage3>();
				}
			}
		}

		/// <summary>
		/// Selects the first tab.
		/// </summary>
		private void SelectFirstTab()
		{
			GuidFramework.Services.NavigationService.MultipageSetSelectedItem<WizardPage1>();
		}

		/// <summary>
		/// Selects the second tab.
		/// </summary>
		private async void SelectSecondTab()
		{
			if (FirstAgeInput.IsNullOrWhiteSpace())
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageInputAgeEmpty);
			else if (FirstAge <= 0)
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageInputAgeInvalidInput);
			else if (FirstAge < Settings.MinimumAgeThreshold)
				await ToastHandler.ShowErrorMessageAsync(Resources.ToastMessageAgeTooLow);
			else
			{
				if (AdsEnabled && maxAds > 0)
				{
					AdHandler.ShowFullScreenAd(BannerAdId, Resources.Loading, Resources.Continue, SecondBannerAdId, () =>
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
					GuidFramework.Services.NavigationService.MultipageSetSelectedItem<WizardPage2>();
				}
			}
		}

		/// <summary>
		/// Resets this instance.
		/// </summary>
		private void Reset()
		{
			SecondAgeInput = string.Empty;

			SelectSecondTab();
		}
	}
}
