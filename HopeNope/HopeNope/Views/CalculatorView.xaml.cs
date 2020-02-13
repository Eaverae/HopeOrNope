using HopeNope.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HopeNope.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CalculatorView : CarouselPage
	{
		private CalculatorViewModel viewModel = new CalculatorViewModel();

		public CalculatorView()
		{
			InitializeComponent();

			if (!viewModel.IsInitialized)
				viewModel.Init();

			BindingContext = viewModel;
		}
	}
}