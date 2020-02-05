using HopeNope.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HopeNope.Handlers
{
	/// <summary>
	/// AlertHandler
	/// </summary>
	/// <seealso cref="Rimek.Framework.App.Interfaces.IAlertHandler" />
	public class AlertHandler : IAlertHandler
	{
		/// <summary>
		/// Displays the action sheet.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="cancel">Text to be displayed in the 'Cancel' button.</param>
		/// <param name="destruction">Text to be displayed in the 'Destruct' button.</param>
		/// <param name="buttons">The buttons.</param>
		/// <returns>String value</returns>
		public async Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons)
		{
			if (title.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(title));

			if (buttons == null)
				throw new ArgumentNullException(nameof(buttons));

			Page page = null;

			if (Application.Current.MainPage.Navigation.NavigationStack != null)
				page = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

			return await page?.DisplayActionSheet(title, cancel, destruction, buttons);
		}

		/// <summary>
		/// Displays the alert.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="message">The message.</param>
		/// <param name="cancel">The cancel.</param>
		/// <returns>Task</returns>
		public async Task DisplayAlertAsync(string title, string message, string cancel)
		{
			if (title.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(title));

			if (message.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(message));

			if (cancel.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(cancel));

			Page page = null;

			if (Application.Current.MainPage.Navigation.NavigationStack != null)
				page = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

			await page?.DisplayAlert(title, message, cancel);
		}

		/// <summary>
		/// Displays the alert.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="message">The message.</param>
		/// <param name="accept">The accept.</param>
		/// <param name="cancel">The cancel.</param>
		/// <returns>Boolean</returns>
		public async Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel)
		{
			if (title.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(title));

			if (message.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(message));

			if (accept.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(accept));

			if (cancel.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(cancel));

			Page page = null;

			if (Application.Current.MainPage.Navigation.NavigationStack != null)
				page = Application.Current.MainPage.Navigation.NavigationStack.LastOrDefault();

			return await page?.DisplayAlert(title, message, accept, cancel);
		}
	}
}
