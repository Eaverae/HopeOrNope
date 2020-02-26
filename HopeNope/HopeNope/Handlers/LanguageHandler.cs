using HopeNope.Entities;
using HopeNope.Interfaces;
using HopeNope.Properties;
using Plugin.Multilingual;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Xamarin.Essentials;

namespace HopeNope.Handlers
{
	/// <summary>
	/// LanguageHandler
	/// </summary>
	/// <seealso cref="HopeNope.Interfaces.ILanguageHandler" />
	public class LanguageHandler : ILanguageHandler
	{
		private const string languageKey = nameof(languageKey);

		private IList<Language> languages;

		/// <summary>
		/// Gets the languages.
		/// </summary>
		/// <returns>
		/// Collection of languages.
		/// </returns>
		public IList<Language> GetLanguages()
		{
			if (languages == null)
			{
				languages = new List<Language>
				{
					new Language {DisplayName = "English", CultureName = "en"},
					new Language {DisplayName = "Nederlands", CultureName = "nl"}
				};
			}

			return languages;
		}

		/// <summary>
		/// Gets the default language.
		/// </summary>
		/// <returns>
		/// Default language.
		/// </returns>
		public Language GetDefaultLanguage()
		{
			// Get the device default language
			string culture = CrossMultilingual.Current.DeviceCultureInfo.Name;

			Language language = GetLanguage(culture);

			if (language == null)
				language = GetLanguages().FirstOrDefault();

			return language;
		}

		/// <summary>
		/// Gets the user language.
		/// </summary>
		/// <returns>Language object</returns>
		public Language GetUserLanguage()
		{
			Language defaultLanguage = GetDefaultLanguage();
			string culture = Preferences.Get(languageKey, defaultLanguage.CultureName);

			Language userLanguage = GetLanguage(culture);

			return userLanguage;
		}

		/// <summary>
		/// Gets a language based on the culture name.
		/// </summary>
		/// <param name="cultureName">Name of the culture.</param>
		/// <returns>
		/// Language.
		/// </returns>
		/// <exception cref="System.ArgumentNullException">cultureName</exception>
		public Language GetLanguage(string cultureName)
		{
			if (cultureName == null)
				throw new ArgumentNullException(nameof(cultureName));

			Language language = GetLanguages().SingleOrDefault(item => item.CultureName == cultureName);

			return language;
		}

		/// <summary>
		/// Sets the language.
		/// </summary>
		/// <param name="culture">The culture.</param>
		/// <exception cref="ArgumentNullException">culture</exception>
		public void SetLanguage(string culture)
		{
			if (culture == null)
				throw new ArgumentNullException(nameof(culture));

			Resources.Culture = new CultureInfo(culture);
			CrossMultilingual.Current.CurrentCultureInfo = Resources.Culture;

			Preferences.Set(languageKey, culture);
		}
	}
}
