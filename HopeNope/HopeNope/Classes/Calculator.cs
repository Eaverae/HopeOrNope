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
		/// <returns></returns>
		public static bool DetermineHopeOrNope(double firstAge, double secondAge)
		{
			double majorAge = firstAge > secondAge ? firstAge : secondAge;
			double minorAge = firstAge > secondAge ? secondAge : firstAge;

			double minimum = Math.Ceiling((majorAge / 2.0) + 7.0);

			return (minimum <= minorAge);

		}
	}
}
