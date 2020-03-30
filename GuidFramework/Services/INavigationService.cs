using GuidFramework.ViewModels;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace GuidFramework.Services
{
	/// <summary>
	/// NavigationService interface
	/// </summary>
	public interface INavigationService
	{
		/// <summary>
		/// Pushes the page to the stack asynchronous.
		/// </summary>
		/// <param name="page">The page.</param>
		/// <returns>Task</returns>
		Task PushAsync(Page page);

		/// <summary>
		/// Pops the asynchronous.
		/// </summary>
		/// <returns>Task</returns>
		Task CloseAsync();

		/// <summary>
		/// Pushes the modal asynchronous.
		/// </summary>
		/// <param name="page">The page.</param>
		/// <returns>Task</returns>
		Task PushModalAsync(Page page);

		/// <summary>
		/// Pops the modal asynchronous.
		/// </summary>
		/// <returns>Page</returns>
		Task<Page> PopModalAsync();

		/// <summary>
		/// Pops to root asynchronous.
		/// </summary>
		/// <param name="animated">if set to <c>true</c> [animated].</param>
		/// <returns>Task</returns>
		Task PopToRootAsync(bool animated = false);

		/// <summary>
		/// Determines whether this instance can navigate.
		/// </summary>
		/// <typeparam name="TViewModelType">The type of the view model type.</typeparam>
		/// <returns>Boolean value</returns>
		bool CanNavigate<TViewModelType>() where TViewModelType : BaseViewModel;

		/// <summary>
		/// Navigates to the specified View that is mapped to the ViewModelType
		/// </summary>
		/// <typeparam name="TViewModelType">The type of the view model type.</typeparam>
		/// <param name="noHistory">if set to <c>true</c> [no history].</param>
		/// <param name="animated">if set to <c>true</c> [animated].</param>
		/// <param name="viewModel">The viewmodel.</param>
		/// <returns>A task</returns>
		Task NavigateAsync<TViewModelType>(bool noHistory = false, bool animated = true, TViewModelType viewModel = null) where TViewModelType : BaseViewModel;
	}
}
