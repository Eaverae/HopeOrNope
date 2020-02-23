using HopeNope.Classes;
using HopeNope.ViewModels;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HopeNope.Services
{
	/// <summary>
	/// NavigationService for navigating from viewmodel to viewmodel
	/// </summary>
	public class NavigationService : INavigationService
	{
		private static bool isNavigating = false;

		/// <summary>
		/// Gets or sets the main page.
		/// </summary>
		/// <value>
		/// The main page.
		/// </value>
		private Page mainPage
		{
			get
			{
				return Application.Current.MainPage;
			}
			set
			{
				Application.Current.MainPage = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance is navigating.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is navigating; otherwise, <c>false</c>.
		/// </value>
		public static bool IsNavigating
		{
			get => isNavigating;
			set
			{
				isNavigating = value;
			}
		}

		/// <summary>
		/// Get the current binding context.
		/// </summary>
		/// <param name="application">The application.</param>
		/// <returns>The base viewmodel</returns>
		public static BaseViewModel CurrentBindingContext(Application application = null)
		{
			// The current binding context can be found in ALL situations 
			if (application == null)
				application = Application.Current;

			Page mainPage = application?.MainPage;

			if (mainPage != null)
			{
				mainPage = (mainPage as MasterDetailPage)?.Detail ?? mainPage;
				mainPage = (mainPage as NavigationPage)?.CurrentPage ?? mainPage;
			}

			return mainPage?.BindingContext as BaseViewModel;
		}

		/// <summary>
		/// Currents the current page as the page of type TPage.
		/// </summary>
		/// <typeparam name="TPage">The type of the page.</typeparam>
		/// <param name="application">The application.</param>
		/// <returns>The page as TPage</returns>
		public static TPage CurrentPage<TPage>(Application application = null)
			where TPage : Page
		{
			// The current binding context can be found in ALL situations 
			if (application == null)
				application = Application.Current;

			Page mainPage = application?.MainPage;

			if (mainPage != null)
			{
				mainPage = (mainPage as MasterDetailPage)?.Detail ?? mainPage;
				mainPage = (mainPage as NavigationPage)?.CurrentPage ?? mainPage;
			}

			return mainPage as TPage;
		}

		/// <summary>
		/// Sets the selected item on the given Multipage object.
		/// </summary>
		/// <typeparam name="TPage">The type of the page.</typeparam>
		/// <param name="automationId">The automation Id of the page</param>
		public static void MultipageSetSelectedItem<TPage>(string automationId = null)
			where TPage : ContentPage
		{
			// Get a reference to the MultiPage
			MultiPage<ContentPage> carouselPage = CurrentPage<MultiPage<ContentPage>>();

			if (carouselPage != null)
			{
				// Find the childpage by type
				TPage resultPage = null;

				if (automationId.IsNullOrWhiteSpace())
					resultPage = (TPage)carouselPage.Children.Single(page => page.GetType().Equals(typeof(TPage)));
				else
					resultPage = (TPage)carouselPage.Children.Single(page => page.GetType().Equals(typeof(TPage)) && page.AutomationId.Equals(automationId));

				// Set the resultpage as the selected item
				carouselPage.SelectedItem = resultPage;
			}
		}

		/// <summary>
		/// Pushes the page to the stack
		/// </summary>
		/// <param name="page">Page to be pushed to the stack</param>
		/// <returns>An awaitable Task</returns>
		public async Task PushAsync(Page page)
		{
			if (page == null)
				throw new ArgumentNullException(nameof(page));

			IsNavigating = true;

			await mainPage.Navigation.PushAsync(page);

			IsNavigating = false;
		}

		/// <summary>
		/// Pops the asynchronous.
		/// </summary>
		/// <returns>
		/// Task
		/// </returns>
		public async Task CloseAsync()
		{
			IsNavigating = true;

			await mainPage.Navigation.PopAsync();

			IsNavigating = false;
		}

		/// <summary>
		/// Pops to root asynchronous.
		/// </summary>
		/// <param name="animated">if set to <c>true</c> [animated].</param>
		/// <returns>
		/// Task
		/// </returns>
		public async Task PopToRootAsync(bool animated = false)
		{
			IsNavigating = true;

			await mainPage.Navigation.PopToRootAsync(animated);

			IsNavigating = false;
		}

		/// <summary>
		/// Pushes the modal page.
		/// </summary>
		/// <param name="page">The page.</param>
		/// <returns>
		/// Task
		/// </returns>
		/// <exception cref="ArgumentNullException">page</exception>
		public async Task PushModalAsync(Page page)
		{
			if (page == null)
				throw new ArgumentNullException(nameof(page));

			IsNavigating = true;

			await mainPage.Navigation.PushModalAsync(page);

			IsNavigating = false;
		}

		/// <summary>
		/// Pop the first modal page from the stack
		/// </summary>
		/// <returns>An awaitable Task</returns>
		public async Task<Page> PopModalAsync()
		{
			IsNavigating = true;

			Page returnValue = await mainPage.Navigation.PopModalAsync();

			IsNavigating = false;

			return returnValue;
		}

		/// <summary>
		/// Determines whether this instance can navigate.
		/// </summary>
		/// <typeparam name="TViewModelType">The type of the view model type.</typeparam>
		/// <returns>Boolean value</returns>
		public bool CanNavigate<TViewModelType>()
			where TViewModelType : BaseViewModel
		{
			bool result = !IsNavigating;

			if (!result)
			{
				Type pageType = ViewFactory.ViewTypeForModel<TViewModelType>();

				if ((mainPage.Navigation.NavigationStack != null && mainPage.Navigation.NavigationStack.Any()) ||
					(mainPage.Navigation.ModalStack != null && mainPage.Navigation.ModalStack.Any()))
				{
					Page page = mainPage.Navigation.NavigationStack.LastOrDefault() ?? mainPage.Navigation.ModalStack.LastOrDefault();

					if (page != null && page.GetType() == pageType)
						result = false;
				}
			}

			return result;
		}

		/// <summary>
		/// Navigates to the specified View that is mapped to the ViewModelType
		/// </summary>
		/// <typeparam name="TViewModelType">The type of the view model type.</typeparam>
		/// <param name="noHistory">if set to <c>true</c> [no history].</param>
		/// <param name="animated">if set to <c>true</c> [animated].</param>
		/// <param name="viewModel">The viewmodel.</param>
		/// <returns>A task</returns>
		public async Task NavigateAsync<TViewModelType>(bool noHistory = false, bool animated = true, TViewModelType viewModel = null)
			where TViewModelType : BaseViewModel
		{
			IsNavigating = true;

			Page page = ViewFactory.CreateView(viewModel);

			// Normal execution
			if (!noHistory)
				await mainPage.Navigation.PushAsync(page, animated);
			else
			{
				if (animated)
					await mainPage.Navigation.PopToRootAsync(animated);

				mainPage = new NavigationPage(page);
			}

			IsNavigating = false;
		}
	}
}
