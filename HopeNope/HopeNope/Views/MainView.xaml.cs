using HopeNope.Classes;
using HopeNope.ViewModels;
using Xamarin.Forms;

namespace HopeNope
{
	/// <summary>
	/// MainView
	/// </summary>
	/// <seealso cref="Xamarin.Forms.ContentPage" />
	public partial class MainView : ContentPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MainView"/> class.
		/// </summary>
		public MainView()
		{
			InitializeComponent();

			// Subscribe to the Language Selected message to refresh the layout
			MessagingCenter.Subscribe<SettingsViewModel>(this, ApplicationConstants.LanguageSelectedMessage, OnLanguageSelected);
		}

		/// <summary>
		/// Called when [language selected].
		/// </summary>
		/// <param name="viewModel">The view model.</param>
		private void OnLanguageSelected(SettingsViewModel viewModel)
		{
			Device.BeginInvokeOnMainThread(() =>
			{
				// Refresh the layout
				UpdateChildrenLayout();
			});
		}
	}
}
