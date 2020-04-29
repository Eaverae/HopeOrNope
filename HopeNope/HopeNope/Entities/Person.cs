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
	}
}
