using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Essentials;

namespace HopeNope.Classes
{
	internal static class Settings
	{
		internal static T GetValue<T>(string resourceKey)
		{
			if (resourceKey.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(resourceKey));

			T returnValue = default(T);

			if (Preferences.ContainsKey(resourceKey))
			{
				string value = Preferences.Get(resourceKey, string.Empty);

				if (!value.IsNullOrWhiteSpace())
					returnValue = (T)Convert.ChangeType(value, typeof(T));
			}

			return returnValue;
		}
	}
}
