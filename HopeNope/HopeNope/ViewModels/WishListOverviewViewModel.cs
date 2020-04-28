using GuidFramework.Interfaces;
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

		/// <summary>
		/// Gets the people.
		/// </summary>
		/// <value>
		/// The people.
		/// </value>
		public IEnumerable<Person> People
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
		public ICommand PersonDetailsCommand => new Command(PersonDetailsAsync);

		/// <summary>
		/// Persons the details asynchronous.
		/// </summary>
		private async void PersonDetailsAsync()
		{
			await AlertHandler.DisplayAlertAsync(Resources.AlertTitleActionNotSupported, Resources.AlertMessageActionNotSupported, Resources.Ok);

			SelectedPerson = null;
		}

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
		private void LoadPeople()
		{
			List<Person> people = new List<Person>();

			bool unlocked = false;
			// Test
			for (int i = 0; i < 5; i++)
			{
				people.Add(new Person(determinedAgeDate: DateTime.Now.AddYears(i * -1), name: $"Hope_{i}", isUnlocked: unlocked));
				unlocked = !unlocked;
			}

			People = people.OrderByDescending(item => item.IsUnlocked);

			OnPropertyChanged(nameof(People));
		}
	}
}
