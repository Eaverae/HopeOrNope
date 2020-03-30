using GuidFramework.Enums;
using GuidFramework.Extensions;
using GuidFramework.Interfaces;
using GuidFramework.Views;
using GuidFramework.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Timers;

namespace GuidFramework.Handlers
{
	/// <summary>
	/// Toasthandler
	/// </summary>
	/// <seealso cref="GuidFramework.Interfaces.IToastHandler" />
	public class ToastHandler : IToastHandler
	{
		private static readonly double interval = 4000;
		private static Timer timer;

		/// <summary>
		/// Gets the font family.
		/// </summary>
		/// <value>
		/// The font family.
		/// </value>
		public static string FontFamily
		{
			get;
			private set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToastHandler"/> class.
		/// </summary>
		/// <param name="fontFamily">The font family.</param>
		public ToastHandler(string fontFamily)
		{
			FontFamily = fontFamily;
		}

		/// <summary>
		/// Shows the error message.
		/// </summary>
		/// <param name="message">The errormessage</param>
		/// <returns>An awaitable task</returns>
		public async Task ShowErrorMessageAsync(string message)
		{
			if (message.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(message));

			await ShowMessageAsync(message, MessageType.Error);
		}

		/// <summary>
		/// Shows the notification message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>An awaitable task</returns>
		public async Task ShowNotificationMessageAsync(string message)
		{
			if (message.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(message));

			await ShowMessageAsync(message, MessageType.Information);
		}

		/// <summary>
		/// Shows the success message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>An awaitable task</returns>
		public async Task ShowSuccessMessageAsync(string message)
		{
			if (message.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(message));

			await ShowMessageAsync(message, MessageType.Success);
		}

		/// <summary>
		/// Shows the warning message.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns>An awaitable task</returns>
		public async Task ShowWarningMessageAsync(string message)
		{
			if (message.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(message));

			await ShowMessageAsync(message, MessageType.Warning);
		}

		/// <summary>
		/// Shows the message asynchronous.
		/// </summary>
		/// <param name="message">The message.</param>
		/// <param name="messageType">Type of the message.</param>
		/// <returns>An awaitable task</returns>
		/// <exception cref="ArgumentNullException">message</exception>
		private async Task ShowMessageAsync(string message, MessageType messageType = MessageType.Information)
		{
			if (message.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(message));

			if (FontFamily.IsNullOrWhiteSpace())
				throw new InvalidOperationException("You need to set the FontFamily before calling any of the ShowMessage members.");

			ToastPopup toast = new ToastPopup
			{
				BindingContext = new ToastPopupViewModel(messageType, message, FontFamily)
			};

			await PopupNavigation.Instance.PushAsync(toast);

			DetectCleanup();
		}

		/// <summary>
		/// Detects the cleanup.
		/// </summary>
		private static void DetectCleanup()
		{
			if (timer == null)
			{
				timer = new Timer
				{
					AutoReset = false,
					Interval = interval
				};
				timer.Elapsed += Timer_Elapsed;
			}
			else
				timer.Stop();

			timer.Start();
		}

		/// <summary>
		/// Handles the Elapsed event of the Timer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="ElapsedEventArgs"/> instance containing the event data.</param>
		private static async void Timer_Elapsed(object sender, ElapsedEventArgs e)
		{
			if (PopupNavigation.Instance.PopupStack.Count > 0)
				await PopupNavigation.Instance.PopAllAsync();

			timer.Stop();
			timer.Dispose();

			timer = null;
		}
	}
}
