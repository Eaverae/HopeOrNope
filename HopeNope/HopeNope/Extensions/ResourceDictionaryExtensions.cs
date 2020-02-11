using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace HopeNope.Extensions
{
	/// <summary>
	/// ResourceDictionaryExtensions
	/// </summary>
	public static class ResourceDictionaryExtensions
	{
		/// <summary>
		/// Gets the platformspecific value for the given key
		/// </summary>
		/// <typeparam name="T">The type of value</typeparam>
		/// <param name="resourceDictionary">The resource dictionary.</param>
		/// <param name="resourceKey">The resource key.</param>
		/// <returns></returns>
		/// <exception cref="ArgumentNullException">
		/// resourceDictionary
		/// or
		/// resourceKey
		/// </exception>
		/// <exception cref="InvalidOperationException">No resource found for given resourcekey.</exception>
		public static T PlatformSpecificValue<T>(this ResourceDictionary resourceDictionary, string resourceKey)
		{
			if (resourceDictionary == null)
				throw new ArgumentNullException(nameof(resourceDictionary));

			if (resourceKey.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(resourceKey));

			if (!resourceDictionary.ContainsKey(resourceKey))
				throw new InvalidOperationException("No resource found for given resourcekey.");

			T returnValue = default;

			OnPlatform<T> onPlatformValues = resourceDictionary[resourceKey] as OnPlatform<T>;

			if (onPlatformValues != null)
			{
				On singleValue = onPlatformValues.Platforms.SingleOrDefault(item => item.Platform.FirstOrDefault().Equals(Device.RuntimePlatform));
				returnValue = (T)singleValue?.Value;
			}

			return returnValue;
		}
	}
}
