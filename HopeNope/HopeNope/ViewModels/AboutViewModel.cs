using HopeNope.Properties;
using Xamarin.Essentials;

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
	}
}