using HopeNope.Handlers;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HopeNope.Views
{
	/// <summary>
	/// CalculatorView
	/// </summary>
	/// <seealso cref="Xamarin.Forms.CarouselPage" />
	public partial class CalculatorView : CarouselPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="CalculatorView"/> class.
		/// </summary>
		public CalculatorView()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Event that is raised when the back button is pressed.
		/// </summary>
		protected override bool OnBackButtonPressed()
		{
			NavigateToFirstTab(this, EventArgs.Empty);

			return true;
		}

		/// <summary>
		/// Navigates to first tab.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void NavigateToFirstTab(object sender, EventArgs e)
		{
			// SelectedItem = Page1;
		}

		/// <summary>
		/// Navigates to second tab.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void NavigateToSecondTab(object sender, EventArgs e)
		{
			// Preload the next ad
			/*AdHandler.LoadInterstitialAd(viewModel.MainTransitionAdId);

			AdHandler.ShowFullScreenAd(viewModel.BannerAdId, () =>
			{
				SelectedItem = Page2;
			});*/
		}

		/// <summary>
		/// Navigates to third tab.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void NavigateToThirdTab(object sender, EventArgs e)
		{
			/*AdHandler.ShowInterstitialAd(() =>
			{
				SelectedItem = ResultPage;
			});*/

		}
	}
}