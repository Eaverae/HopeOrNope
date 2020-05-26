using GuidFramework;
using GuidFramework.Extensions;
using Newtonsoft.Json;
using System;

namespace HopeNope.Entities
{
	/// <summary>
	/// CalculatedResult entity. Used for statistics
	/// </summary>
	public class CalculatedResult : NotifyPropertyChanged
	{
		/// <summary>
		/// Gets or sets the age.
		/// </summary>
		/// <value>
		/// The age.
		/// </value>
		public double UserAge
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
		/// Gets or sets the determined date.
		/// </summary>
		/// <value>
		/// The determined date.
		/// </value>
		public DateTime DeterminedDate
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating whether this <see cref="CalculatedResult"/> is verdict.
		/// </summary>
		/// <value>
		///   <c>true</c> if verdict; otherwise, <c>false</c>.
		/// </value>
		public bool Verdict
		{
			get;
			set;
		}

		/// <summary>
		/// Froms the json.
		/// </summary>
		/// <param name="instance">The instance.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">instance</exception>
		public static CalculatedResult FromJson(string instance)
		{
			if (instance.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(instance));

			return JsonConvert.DeserializeObject<CalculatedResult>(instance);
		}

		/// <summary>
		/// Converts to person.
		/// </summary>
		/// <param name="name">The name of the person</param>
		/// <returns>Person object</returns>
		public Person ToPerson(string name)
		{
			if (name.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(name));

			return new Person()
			{
				UserAge = UserAge,
				Age = CompareAge,
				DeterminedAgeDate = DeterminedDate,
				DisplayName = name
			};
		}

		/// <summary>
		/// Converts to string.
		/// </summary>
		/// <returns>
		/// A <see cref="System.String" /> that represents this instance.
		/// </returns>
		public override string ToString()
		{
			return JsonConvert.SerializeObject(this);
		}
	}
}
