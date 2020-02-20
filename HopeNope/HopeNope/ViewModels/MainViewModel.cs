using Autofac;
using HopeNope.Interfaces;
using HopeNope.Views;
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
			await Application.Current.MainPage.Navigation.PushAsync(new CalculatorView());
		});

		/// <summary>
		/// Gets the about command.
		/// </summary>
		/// <value>
		/// The about command.
		/// </value>
		public ICommand AboutCommand => new Command(async () =>
		{
			await Application.Current.MainPage.Navigation.PushAsync(new AboutView());
		});

		public ICommand RemoveAdsCommand => new Command(async () =>
		{
			if (await PurchaseHandler.WasItemPurchased())
				await AlertHandler.DisplayAlertAsync("Item already purchased", "Item already purchased", "Ok");
			else
			{
				if (await PurchaseHandler.MakePurchase())
					await ToastHandler.ShowSuccessMessageAsync("Great success!");
			}
		});


		public MainViewModel()
		{
			using (ILifetimeScope scope = GuidApp.Container.BeginLifetimeScope())
			{
				PurchaseHandler = scope.Resolve<IPurchaseHandler>();
			}
		}
	}
}