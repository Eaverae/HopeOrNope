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
		public string MainTransitionAdId
		{
			get
			{
				return "ca-app-pub-3950359454148049/3683332556";
			}
		}

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

		});
	}
}