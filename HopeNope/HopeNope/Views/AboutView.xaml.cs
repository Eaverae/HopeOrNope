using HopeNope.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HopeNope.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AboutView : ContentPage
	{
		public AboutView()
		{
			InitializeComponent();

			BindingContext = new AboutViewModel();
		}
	}
}