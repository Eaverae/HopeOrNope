using System.Threading.Tasks;

namespace HopeNope.Interfaces
{
	/// <summary>
	/// IAlertHandler
	/// </summary>
	public interface IAlertHandler
	{
		/// <summary>
		/// Displays the action sheet.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="cancel">Text to be displayed in the 'Cancel' button.</param>
		/// <param name="destruction">Text to be displayed in the 'Destruct' button.</param>
		/// <param name="buttons">The buttons.</param>
		/// <returns></returns>
		Task<string> DisplayActionSheetAsync(string title, string cancel, string destruction, params string[] buttons);

		/// <summary>
		/// Displays the alert.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="message">The message.</param>
		/// <param name="cancel">The cancel.</param>
		/// <returns></returns>
		Task DisplayAlertAsync(string title, string message, string cancel);

		/// <summary>
		/// Displays the alert.
		/// </summary>
		/// <param name="title">The title.</param>
		/// <param name="message">The message.</param>
		/// <param name="accept">The accept.</param>
		/// <param name="cancel">The cancel.</param>
		/// <returns></returns>
		Task<bool> DisplayAlertAsync(string title, string message, string accept, string cancel);
	}
}
