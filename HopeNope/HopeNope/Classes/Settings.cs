using HopeNope.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Essentials;

namespace HopeNope.Classes
{
	/// <summary>
	/// Settings class which holds all the settings for the app
	/// </summary>
	internal static class Settings
	{
		private const string statisticsKey = "statistics";
		private const string adsEnabledKey = "adsEnabled";
		private const string dateOfBirthKey = "dateOfBirth";
		private const string personalizedAdsKey = "personalizedAds";
		private const string thresHoldKey = "thresHold";

		/// <summary>
		/// Gets the current statistics.
		/// </summary>
		/// <value>
		/// The current statistics.
		/// </value>
		internal static IList<CalculatedResult> CurrentStatistics
		{
			get
			{
				IList<CalculatedResult> results = null;

				string statsSettings = Preferences.Get(statisticsKey, string.Empty);

				if (!statsSettings.IsNullOrWhiteSpace())
					results = JsonConvert.DeserializeObject<List<CalculatedResult>>(statsSettings);

				return results;
			}
		}

		/// <summary>
		/// Saves the statistic.
		/// </summary>
		/// <param name="statistic">The statistic.</param>
		internal static void SaveStatistic(CalculatedResult statistic)
		{
			if (statistic != null)
			{
				IList<CalculatedResult> statistics = CurrentStatistics ?? new List<CalculatedResult>();
				statistics.Add(statistic);

				Preferences.Set(statisticsKey, JsonConvert.SerializeObject(statistics));
			}
		}

		/// <summary>
		/// Gets or sets the minimum age threshold.
		/// </summary>
		/// <value>
		/// The minimum age threshold.
		/// </value>
		internal static int MinimumAgeThreshold
		{
			get
			{
				return Preferences.Get(thresHoldKey, 16);
			}
			set
			{
				Preferences.Set(thresHoldKey, value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether [ads enabled].
		/// </summary>
		/// <value>
		///   <c>true</c> if [ads enabled]; otherwise, <c>false</c>.
		/// </value>
		internal static bool AdsEnabled
		{
			get
			{
				return Preferences.Get(adsEnabledKey, true);
			}
			set
			{
				Preferences.Set(adsEnabledKey, value);
			}
		}

		/// <summary>
		/// Gets or sets a value indicating whether this instance has default age.
		/// </summary>
		/// <value>
		///   <c>true</c> if this instance has default age; otherwise, <c>false</c>.
		/// </value>
		internal static bool HasDefaultAge
		{
			get
			{
				return !DefaultAge.IsNullOrWhiteSpace();
			}
		}

		/// <summary>
		/// Gets or sets the default age.
		/// </summary>
		/// <value>
		/// The default age.
		/// </value>
		internal static string DefaultAge
		{
			get
			{
				string returnvalue = string.Empty;

				if (DateOfBirth != null && DateOfBirth.HasValue)
					returnvalue = CalculateAge(DateOfBirth).ToString();

				return returnvalue;
			}
		}

		/// <summary>
		/// Gets or sets the date of birth.
		/// </summary>
		/// <value>
		/// The date of birth.
		/// </value>
		internal static DateTime? DateOfBirth
		{
			get
			{
				long ticks = Preferences.Get(dateOfBirthKey, (long)0);

				return ticks > 0 ? new DateTime(ticks) : (DateTime?)null;
			}
			set
			{
				if (value != null && value.HasValue)
					Preferences.Set(dateOfBirthKey, value.Value.Ticks);
			}
		}


		/// <summary>
		/// Gets or sets a value indicating whether [user personalized ads].
		/// </summary>
		/// <value>
		///   <c>true</c> if [user personalized ads]; otherwise, <c>false</c>.
		/// </value>
		internal static bool UserPersonalizedAds
		{
			get
			{
				return Preferences.Get(personalizedAdsKey, false);
			}
			set
			{
				Preferences.Set(personalizedAdsKey, value);
			}
		}


		/// <summary>
		/// Calculates the age.
		/// </summary>
		/// <param name="dateOfBirth">The date of birth.</param>
		/// <returns></returns>
		private static int CalculateAge(DateTime? dateOfBirth)
		{
			if (dateOfBirth == null)
				throw new ArgumentNullException(nameof(dateOfBirth));

			int returnValue = 0;

			if (dateOfBirth > DateTime.MinValue && dateOfBirth < DateTime.MaxValue)
			{
				// Get current date (don't call DateTime.Today repeatedly, as it changes)
				DateTime today = DateTime.Today;

				// Get the last birthday
				int years = today.Year - dateOfBirth.Value.Year;

				DateTime last = dateOfBirth.Value.AddYears(years);
				if (last > today)
				{
					last = last.AddYears(-1);
					years--;
				}

				// Get the next birthday
				DateTime next = last.AddYears(1);

				// Calculate the number of days between them
				double yearDays = (next - last).Days;

				// Calcluate the number of days since last birthday
				double days = (today - last).Days;

				// Calculate exaxt age
				double exactAge = (double)years + (days / yearDays);

				returnValue = (int)Math.Floor(exactAge);
			}

			return returnValue;
		}
	}
}
