using HopeNope.ViewModels;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HopeNope.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalculatorView : CarouselPage
	{
		private int maxAds = 3;

		private CalculatorViewModel viewModel = new CalculatorViewModel();

		public CalculatorView()
		{
			InitializeComponent();

			if (!viewModel.IsInitialized)
				viewModel.Init();

			BindingContext = viewModel;
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
			SelectedItem = Page1;
		}

		/// <summary>
		/// Navigates to second tab.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void NavigateToSecondTab(object sender, EventArgs e)
		{
			SelectedItem = Page2;
		}

		/// <summary>
		/// Navigates to third tab.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
		private void NavigateToThirdTab(object sender, EventArgs e)
		{
			SelectedItem = ResultPage;
		}
	}
}