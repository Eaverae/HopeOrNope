using HopeNope.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace HopeNope.Classes
{
	/// <summary>
	/// ViewFactory
	/// </summary>
	public static class ViewFactory
	{
		/// <summary>
		/// Returns the ViewModel-View Dictionary with the ViewModel-type as key
		/// <para>Please note that this is explicitly set to a private static readonly variable.</para>
		/// </summary>
		private static readonly IDictionary<Type, Type> viewModelViewDictionary = new Dictionary<Type, Type>();

		/// <summary>
		/// Register a View with ViewModel
		/// </summary>
		/// <typeparam name="TView">Type of View to register</typeparam>
		/// <typeparam name="TViewModel">Type of ViewModel to register</typeparam>
		public static void RegisterView<TView, TViewModel>()
			where TView : Page
			where TViewModel : BaseViewModel
		{
			if (viewModelViewDictionary.ContainsKey(typeof(TViewModel)))
				throw new InvalidOperationException("The ViewModel has already been registered.");

			if (viewModelViewDictionary.Values.Contains(typeof(TView)))
				throw new InvalidOperationException("The View has already been registered.");

			// Add the ViewModel-View to the dictionary
			viewModelViewDictionary[typeof(TViewModel)] = typeof(TView);
		}

		/// <summary>
		/// Deregister a ViewModel
		/// </summary>
		/// <typeparam name="TViewModel">Type of ViewModel to deregister</typeparam>
		public static void DeregisterView<TViewModel>()
			where TViewModel : BaseViewModel
		{
			if (viewModelViewDictionary.ContainsKey(typeof(TViewModel)))
				viewModelViewDictionary.Remove(typeof(TViewModel));
		}

		/// <summary>
		/// Deregister all ViewModels
		/// </summary>
		public static void DeregisterAll()
		{
			viewModelViewDictionary.Clear();
		}

		/// <summary>
		/// Creates a Page object and returns the Page, based on the given View and ViewModel class
		/// </summary>
		/// <param name="viewModel">[Optional] Init method</param>
		/// <param name="args">[Optional] The view arguments</param>
		/// <typeparam name="TViewModel">Type of ViewModel to create the page for</typeparam>
		/// <returns>A Page object</returns>
		public static Page CreateView<TViewModel>(TViewModel viewModel = null, params object[] args)
			where TViewModel : BaseViewModel
		{
			Type viewModelType = typeof(TViewModel);

			if (!viewModelViewDictionary.ContainsKey(viewModelType))
				throw new InvalidOperationException("No registration found for viewmodel {0}".FormatInvariant(viewModelType.Name));

			// Get the type from the dictionary
			Type viewType = viewModelViewDictionary[viewModelType];

			Page view = null;
			try
			{
				// Resolve or create the viewmodel:
				view = (Page)Activator.CreateInstance(viewType, args);
				viewModel = viewModel ?? Activator.CreateInstance<TViewModel>();
			}
			catch (Exception exception)
			{
				throw new InvalidOperationException("Could not create instance of type {0}".FormatInvariant(viewModelType.ToString()), exception);
			}

			view.Appearing += viewModel.OnAppearing;
			view.Disappearing += viewModel.OnDisappearing;

			if (!viewModel.IsInitialized)
				viewModel.Init();

			// Set the BindingContext
			view.BindingContext = viewModel;

			return view;
		}

		/// <summary>
		/// Views the type for model.
		/// </summary>
		/// <typeparam name="TViewModel">The type of the view model.</typeparam>
		/// <returns></returns>
		/// <exception cref="InvalidOperationException">No registration found for viewmodel {typeof(TViewModel).Name}</exception>
		public static Type ViewTypeForModel<TViewModel>()
		{
			Type returnType = null;

			if (viewModelViewDictionary.ContainsKey(typeof(TViewModel)))
				returnType = viewModelViewDictionary[typeof(TViewModel)];
			else
				throw new InvalidOperationException($"No registration found for viewmodel {typeof(TViewModel).Name}.");

			return returnType;
		}
	}
}
