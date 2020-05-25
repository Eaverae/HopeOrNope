using System;
using System.Reflection;

namespace HopeNope.Classes
{
	/// <summary>
	/// Calculator class
	/// </summary>
	public static class Calculator
	{
		/// <summary>
		/// Determines the hope or nope.
		/// </summary>
		/// <param name="firstAge">The first age.</param>
		/// <param name="secondAge">The second age.</param>
		/// <returns>A boolean value</returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// firstAge
		/// or
		/// secondAge
		/// </exception>
		public static bool DetermineHopeOrNope(double firstAge, double secondAge)
		{
			if (firstAge <= 0)
				throw new ArgumentOutOfRangeException(nameof(firstAge));

			if (secondAge <= 0)
				throw new ArgumentOutOfRangeException(nameof(secondAge));

			double majorAge = firstAge > secondAge ? firstAge : secondAge;
			double minorAge = firstAge > secondAge ? secondAge : firstAge;

			double minimum = Math.Ceiling((majorAge / 2.0) + 7.0);

			return (minimum <= minorAge);
		}

		/// <summary>
		/// Determines the unlock years.
		/// </summary>
		/// <param name="firstAge">The first age.</param>
		/// <param name="secondAge">The second age.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentOutOfRangeException">
		/// firstAge
		/// or
		/// secondAge
		/// </exception>
		public static double DetermineUnlockYears(double firstAge, double secondAge)
		{
			if (firstAge <= 0)
				throw new ArgumentOutOfRangeException(nameof(firstAge));

			if (secondAge <= 0)
				throw new ArgumentOutOfRangeException(nameof(secondAge));

			//Max(L1 - 2 * L2 + 14; L2 - 2 * L1 + 14; 0)

			var result1 = firstAge - 2 * secondAge + 14;
			var result2 = secondAge - 2 * firstAge + 14;
			var result3 = Math.Max(result1, result2);

			return Math.Max(result3, 0);
		}
	}
}
