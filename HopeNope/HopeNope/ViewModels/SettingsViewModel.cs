using HopeNope.Entities;
using HopeNope.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HopeNope.ViewModels
{
	/// <summary>
	/// AboutViewModel
	/// </summary>
	/// <seealso cref="HopeNope.ViewModels.BaseViewModel" />
	public class SettingsViewModel : BaseViewModel
	{
		private readonly ILanguageHandler languageHandler;
		private Language selectedLanguage;

		/// <summary>
		/// Gets the languages.
		/// </summary>
		/// <value>
		/// The languages.
		/// </value>
		public IList<Language> Languages
		{
			get;
			private set;
		}
		
		/// <summary>
		/// Gets or sets the selected language.
		/// </summary>
		/// <value>
		/// The selected language.
		/// </value>
		public Language SelectedLanguage
		{
			get => selectedLanguage;
			set
			{
				// Async setter
				SetLanguage(value);

				OnPropertyChanged();
			}
		}

		/// <summary>
		/// Initializes this instance.
		/// <para>Sets IsInitialized to true</para>
		/// </summary>
		public override void Init()
		{
			base.Init();

			Languages = languageHandler.GetLanguages();
		}

		/// <summary>
		/// Sets the language.
		/// </summary>
		/// <param name="value">The value.</param>
		private async void SetLanguage(Language value)
		{
			if (value != selectedLanguage)
			{
				Language temp = selectedLanguage;
				selectedLanguage = value;

				// Set the language when the user confirms the change
				if (await SaveLanguageAsync() == false)
					selectedLanguage = temp;
			}
		}

		/// <summary>
		/// Saves the language asynchronous.
		/// </summary>
		/// <returns>An awaitable task</returns>
		private async Task<bool> SaveLanguageAsync()
		{
			bool result = false;

			/*if (selectedLanguage != null)
				result = await AlertHandler.DisplayAlertAsync(AppResources.AlertTitleAreYouSure, AppResources.AlertMessageSettingLanguageLogoutWarning, AppResources.Ok, AppResources.Cancel);

			if (result)
			{
				languageHandler.SetLanguage(selectedLanguage.CultureName);

				await ToastHandler.ShowNotificationMessageAsync(AppResources.ToastMessageLanguageSet);

				// Send the message to the app that the language has been changed.
				MessagingCenter.Send(this, ApplicationConstants.LanguageSelectedMessage);

				await LogoutAsync();
			}*/

			return result;
		}
	}
}