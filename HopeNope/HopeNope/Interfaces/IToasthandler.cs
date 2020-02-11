using System.Threading.Tasks;

namespace HopeNope.Interfaces
{
	/// <summary>
	/// The Toasthandler interface
	/// </summary>
	public interface IToastHandler
	{
		/// <summary>
		/// Shows the notification message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>An awaitable task</returns>
		Task ShowNotificationMessageAsync(string message);

		/// <summary>
		/// Shows the success message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>An awaitable task</returns>
		Task ShowSuccessMessageAsync(string message);

		/// <summary>
		/// Shows the warning message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>An awaitable task</returns>
		Task ShowWarningMessageAsync(string message);

		/// <summary>
		/// Shows the error message.
		/// </summary>
		/// <param name="message">The errormessage</param>
		/// <returns>An awaitable task</returns>
		Task ShowErrorMessageAsync(string message);
	}
}
