using GuidFramework.Classes;
using System;

namespace HopeNope.Entities
{
	/// <summary>
	/// Person entity
	/// </summary>
	public class Person : BaseEntity
	{
		/// <summary>
		/// Gets a value indicating whether this instance is unlocked.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance is unlocked; otherwise, <c>false</c>.
		/// </value>
		public bool IsUnlocked
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the determined age date.
		/// </summary>
		/// <value>
		/// The determined age date.
		/// </value>
		public DateTime DeterminedAgeDate
		{
			get;
			private set;
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
			private set;
		}

		/// <summary>
		/// Gets the display name.
		/// </summary>
		/// <value>
		/// The display name.
		/// </value>
		public string DisplayName
		{
			get;
			private set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="Person"/> class.
		/// </summary>
		/// <param name="determinedAgeDate">The determined age date.</param>
		/// <param name="name">The name.</param>
		/// <param name="isUnlocked">Indicates whether or not the person has been unlocked</param>
		public Person(DateTime determinedAgeDate, string name, bool isUnlocked = false)
		{
			DeterminedAgeDate = determinedAgeDate;
			DisplayName = name;
			IsUnlocked = isUnlocked;
		}
	}
}
