using HopeNope.Properties;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// AboutViewModel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.BaseViewModel" />
	public class AboutViewModel : BaseViewModel
	{
		/// <summary>
		/// Gets the current version.
		/// </summary>
		/// <value>
		/// The current version.
		/// </value>
		public string CurrentVersion
		{
			get
			{
				return $"{Resources.CurrentVersion}: {VersionTracking.CurrentVersion}";
			}
		}

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
	}
}