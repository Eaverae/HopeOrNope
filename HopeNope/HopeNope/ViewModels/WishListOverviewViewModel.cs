﻿using Autofac;
using GuidFramework.Interfaces;
using GuidFramework.Services;
using HopeNope.Entities;
using HopeNope.Properties;
using HopeNope.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// WishlistOverviewViewModel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.Base.HopeNopeViewModel" />
	public class WishListOverviewViewModel : HopeNopeViewModel, IListViewModel
	{
		private Person selectedPerson;
		private ILocalStorageHandler localStorageHandler;

		/// <summary>
		/// Gets the people.
		/// </summary>
		/// <value>
		/// The people.
		/// </value>
		public List<Person> People
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets or sets the selected person.
		/// </summary>
		/// <value>
		/// The selected person.
		/// </value>
		public Person SelectedPerson
		{
			get
			{
				return selectedPerson;
			}
			set
			{
				selectedPerson = value;

				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Gets the person details command.
		/// </summary>
		/// <value>
		/// The person details command.
		/// </value>
		public ICommand PersonDetailsCommand => new Command(PersonDetailsAsync, CanExecuteCommands);

		/// <summary>
		/// Gets the clear command.
		/// </summary>
		/// <value>
		/// The clear command.
		/// </value>
		public ICommand ClearCommand => new Command(ClearWishlistAsync, CanExecuteCommands);

		/// <summary>
		/// Gets a value indicating whether this instance has items.
		/// </summary>
		/// <value>
		/// <c>true</c> if this instance has items; otherwise, <c>false</c>.
		/// </value>
		public bool HasItems
		{
			get
			{
				return People != null && People.Any();
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="WishListOverviewViewModel"/> class.
		/// </summary>
		public WishListOverviewViewModel()
		{
			using (ILifetimeScope scope = App.Container.BeginLifetimeScope())
			{
				localStorageHandler = scope.Resolve<ILocalStorageHandler>();
			}
		}

		/// <summary>
		/// Initializes this instance.
		/// <para>Sets IsInitialized to true</para>
		/// </summary>
		public override void Init()
		{
			base.Init();

			LoadPeople();
		}

		/// <summary>
		/// Loads the people.
		/// </summary>
		/// <exception cref="NotImplementedException"></exception>
		private async void LoadPeople()
		{
			IEnumerable<Person> result = await localStorageHandler.ListAsync<Person>();

			if (result != null && result.Any())
				People = result.OrderByDescending(item => item.IsUnlocked).ToList();

			OnPropertyChanged(nameof(People));
			OnPropertyChanged(nameof(HasItems));
		}

		/// <summary>
		/// Clears the wishlist.
		/// </summary>
		private async void ClearWishlistAsync()
		{
			SelectedPerson = null;

			if (await AlertHandler.DisplayAlertAsync(Resources.AlertTitleAreYouSure, Resources.AlertMessageAreYouSure, Resources.Ok, Resources.Cancel))
			{
				DependencyService.Get<IFileService>().ClearInternalStorageFolder();
				LoadPeople();
			}
		}

		/// <summary>
		/// Persons the details asynchronous.
		/// </summary>
		private async void PersonDetailsAsync()
		{
			await AlertHandler.DisplayAlertAsync(Resources.AlertTitleActionNotSupported, Resources.AlertMessageActionNotSupported, Resources.Ok);

			SelectedPerson = null;
		}
	}
}
