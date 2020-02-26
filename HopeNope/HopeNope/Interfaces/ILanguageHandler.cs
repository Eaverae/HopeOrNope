using HopeNope.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace HopeNope.Interfaces
{
	/// <summary>
	/// LanguageHandler interface
	/// </summary>
	public interface ILanguageHandler
	{
		/// <summary>
		/// Gets the languages.
		/// </summary>
		/// <returns></returns>
		IList<Language> GetLanguages();

		/// <summary>
		/// Gets the default language.
		/// </summary>
		/// <returns></returns>
		Language GetDefaultLanguage();

		/// <summary>
		/// Gets the user language.
		/// </summary>
		/// <returns></returns>
		Language GetUserLanguage();

		/// <summary>
		/// Gets the language.
		/// </summary>
		/// <param name="culture">The culture.</param>
		/// <returns></returns>
		Language GetLanguage(string culture);

		/// <summary>
		/// Sets the language.
		/// </summary>
		/// <param name="culture">The culture.</param>
		void SetLanguage(string culture);
	}
}
