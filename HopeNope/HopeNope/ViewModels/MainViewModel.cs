using Autofac;
using HopeNope.Classes;
using HopeNope.Interfaces;
using HopeNope.Properties;
using System;
using System.Windows.Input;
using Xamarin.Forms;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// MainViewModel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.BaseViewModel" />
	public class MainViewModel : BaseViewModel
	{
		/// <summary>
		/// Gets the purchase handler.
		/// </summary>
		/// <value>
		/// The purchase handler.
		/// </value>
		public IPurchaseHandler PurchaseHandler { get; private set; }

		/// <summary>
		/// The back command
		/// </summary>
		public ICommand StartCommand => new Command(async () =>
		{
			await NavigationService.NavigateAsync<CalculatorViewModel>(animated: false);
		});

		/// <summary>
		/// Gets the about command.
		/// </summary>
		/// <value>
		/// The about command.
		/// </value>
		public ICommand AboutCommand => new Command(async () =>
		{
			await NavigationService.NavigateAsync<AboutViewModel>(animated: false);
		});

		/// <summary>
		/// Gets the settings command.
		/// </summary>
		/// <value>
		/// The settings command.
		/// </value>
		public ICommand SettingsCommand => new Command(async () =>
		{
			await NavigationService.NavigateAsync<SettingsViewModel>(animated: false);
		});

		/// <summary>
		/// Gets the remove ads command.
		/// </summary>
		/// <value>
		/// The remove ads command.
		/// </value>
		public ICommand RemoveAdsCommand => new Command(async () =>
		{
			if (await PurchaseHandler.WasItemPurchased(ApplicationConstants.ProductId))
			{
				Settings.AdsEnabled = false;
				await AlertHandler.DisplayAlertAsync(Resources.AlertTitleItemAlreadyPurchased, Resources.AlertMessageItemAlreadyPurchased, Resources.Ok);
			}
			else
			{
				if (await PurchaseHandler.MakePurchase(ApplicationConstants.ProductId, ApplicationConstants.ProductPayLoad))
				{
					// Success
					Settings.AdsEnabled = false;

					await ToastHandler.ShowSuccessMessageAsync(Resources.ToastMessagePurchaseSuccess);

					OnPropertyChanged(nameof(AdsEnabled));
				}
			}
		});

		/// <summary>
		/// Initializes a new instance of the <see cref="MainViewModel"/> class.
		/// </summary>
		public MainViewModel()
		{
			using (ILifetimeScope scope = GuidApp.Container.BeginLifetimeScope())
			{
				PurchaseHandler = scope.Resolve<IPurchaseHandler>();
			}
		}
	}
}