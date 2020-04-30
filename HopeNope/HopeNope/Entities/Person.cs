using GuidFramework.Classes;
using HopeNope.Classes;
using System;

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
				DateTime current = DateTime.Now;
				int year = new DateTime(current.Subtract(DeterminedAgeDate).Ticks).Year - 1;

				double age = Age;

				if (year > 0)
					age += year;

				return Calculator.DetermineHopeOrNope(CompareAge, age);
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
		/// Gets or sets the compare age.
		/// </summary>
		/// <value>
		/// The compare age.
		/// </value>
		public double CompareAge
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
	}
}
