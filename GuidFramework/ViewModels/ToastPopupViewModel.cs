using GuidFramework.Enums;
using GuidFramework.Extensions;
using System;

namespace GuidFramework.ViewModels
{
	/// <summary>
	/// The Viewmodel for the ToastPopup
	/// </summary>
	public class ToastPopupViewModel
	{
		/// <summary>
		/// Gets the font family.
		/// </summary>
		/// <value>
		/// The font family.
		/// </value>
		public string FontFamily
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the message.
		/// </summary>
		/// <value>
		/// The message.
		/// </value>
		public string Message
		{
			get;
			private set;
		}

		/// <summary>
		/// Gets the type of the message.
		/// </summary>
		/// <value>
		/// The type of the message.
		/// </value>
		public MessageType MessageType
		{
			get;
			private set;
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ToastPopupViewModel"/> class.
		/// </summary>
		/// <param name="messageType">Type of the message.</param>
		/// <param name="message">The message.</param>
		/// <param name="fontFamily">The font family</param>
		public ToastPopupViewModel(MessageType messageType, string message, string fontFamily)
		{
			if (message.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(message));

			if (fontFamily.IsNullOrWhiteSpace())
				throw new ArgumentNullException(nameof(fontFamily));

			Message = message;
			MessageType = messageType;
			FontFamily = fontFamily;
		}
	}
}
