using GuidFramework.Classes;
using HopeNope.Classes;
using HopeNope.Properties;
using Plugin.Media.Abstractions;
using System;
using Xamarin.Forms;

namespace HopeNope.Entities
{
	/// <summary>
	/// Person entity
	/// </summary>
	public class Person : BaseEntity
	{
		#region Properties

		/// <summary>
		/// Gets a value indicating whether this instance is unlocked.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is unlocked; otherwise, <c>false</c>.
		/// </value>
		public bool IsUnlocked
		{
			get
			{
				double currentAge = DetermineCurrentAge();

				return Calculator.DetermineHopeOrNope(UserAge, currentAge);
			}
		}

		/// <summary>
		/// Gets or sets the determined age date.
		/// </summary>
		/// <value>
		/// The determined age date.
		/// </value>
		public DateTime UnlockDate
		{
			get
			{
				DateTime dateTime = DateTime.Now.Date;
				double currentAge = DetermineCurrentAge();

				double numberOfYears = Calculator.DetermineUnlockYears(UserAge, currentAge);

				var days = Math.Ceiling(numberOfYears * 365.4);

				return dateTime.Add(TimeSpan.FromDays(days));
			}
		}

		/// <summary>
		/// Gets the countdown.
		/// </summary>
		/// <value>
		/// The countdown.
		/// </value>
		public string Countdown
		{
			get
			{
				TimeSpan timeSpan = UnlockDate.Subtract(DateTime.Now);

				return $"{timeSpan.Days} {Resources.Days}, {timeSpan.Hours} {Resources.Hours}, {timeSpan.Minutes} {Resources.Minutes}, {timeSpan.Seconds} {Resources.Seconds}";
			}
		}

		/// <summary>
		/// Gets or sets the determined age date.
		/// </summary>
		/// <value>
		/// The determined age date.
		/// </value>
		public DateTime DeterminedAgeDate
		{
			get;
			set;
		}

		/// <summary>
		/// Gets the current age.
		/// </summary>
		/// <value>
		/// The current age.
		/// </value>
		public int CurrentAge
		{
			get
			{
				return (int)Math.Floor(DetermineCurrentAge());
			}
		}

		/// <summary>
		/// Gets or sets the user age.
		/// </summary>
		/// <value>
		/// The user age.
		/// </value>
		public double UserAge
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the age.
		/// </summary>
		/// <value>
		/// The age.
		/// </value>
		public double Age
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets the display name.
		/// </summary>
		/// <value>
		/// The display name.
		/// </value>
		public string DisplayName
		{
			get;
			set;
		}

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="Person"/> class.
		/// </summary>
		public Person()
		{
			Id = Guid.NewGuid().ToString();
		}

		/// <summary>
		/// Refreshes this instance.
		/// </summary>
		public void Refresh()
		{
			OnPropertyChanged(nameof(IsUnlocked));
			OnPropertyChanged(nameof(UnlockDate));
			OnPropertyChanged(nameof(Countdown));
		}

		/// <summary>
		/// Determines the current age.
		/// </summary>
		/// <returns>A double value</returns>
		private double DetermineCurrentAge()
		{
			DateTime current = DateTime.Now;
			int year = new DateTime(current.Subtract(DeterminedAgeDate).Ticks).Year - 1;

			double age = Age;

			if (year > 0)
				age += year;

			return age;
		}
	}
}
