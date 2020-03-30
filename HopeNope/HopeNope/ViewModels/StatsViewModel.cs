using HopeNope.Classes;
using HopeNope.Entities;
using HopeNope.ViewModels.Base;
using System.Collections.Generic;
using System.Linq;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// StatsViewModel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.BaseViewModel" />
	public class StatsViewModel : HopeNopeViewModel
	{
		IList<CalculatedResult> statistics = null;

		/// <summary>
		/// Gets the number of hope.
		/// </summary>
		/// <value>
		/// The number of hope.
		/// </value>
		public int NumberOfHope
		{
			get
			{
				return statistics?.Count(item => item.Verdict) ?? 0;
			}
		}

		/// <summary>
		/// Gets the number of nope.
		/// </summary>
		/// <value>
		/// The number of nope.
		/// </value>
		public int NumberOfNope
		{
			get
			{
				return Total - NumberOfHope;
			}
		}

		/// <summary>
		/// Gets the total.
		/// </summary>
		/// <value>
		/// The total.
		/// </value>
		public int Total
		{
			get
			{
				return statistics?.Count() ?? 0;
			}
		}

		/// <summary>
		/// Gets the total.
		/// </summary>
		/// <value>
		/// The total.
		/// </value>
		public string LastAttempt
		{
			get
			{
				string returnValue = string.Empty;

				if (Total > 0)
					returnValue = statistics.LastOrDefault().DeterminedDate.ToString();

				return returnValue;
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="StatsViewModel"/> class.
		/// </summary>
		public StatsViewModel()
		{
			statistics = Settings.CurrentStatistics;

			OnPropertyChanged(nameof(NumberOfHope));
			OnPropertyChanged(nameof(NumberOfNope));
			OnPropertyChanged(nameof(Total));
			OnPropertyChanged(nameof(LastAttempt));
		}
	}
}