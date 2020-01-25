using System;
using System.Collections.Generic;
using System.Text;

namespace HopeNope.Classes
{
	public static class StringExtensions
	{
		public static bool IsNullOrWhiteSpace(this string instance)
		{
			return String.IsNullOrWhiteSpace(instance);
		}
	}
}
