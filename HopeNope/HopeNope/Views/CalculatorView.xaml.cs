using Xamarin.Forms;

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
			GuidFramework.Services.NavigationService.MultipageSetSelectedItem<WizardPage1>();

			return true;
		}
	}
}