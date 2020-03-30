using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;

namespace GuidFramework.Views
{
	/// <summary>
	/// The toastpopup
	/// </summary>
	/// <seealso cref="Rg.Plugins.Popup.Pages.PopupPage" />
	public partial class ToastPopup : PopupPage
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ToastPopup"/> class.
		/// </summary>
		public ToastPopup()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Handles the Tapped event of the TapGestureRecognizer control.
		/// </summary>
		/// <param name="sender">The source of the event.</param>
		/// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
		private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
		{
			if (PopupNavigation.Instance.PopupStack.Count > 0)
				await PopupNavigation.Instance.PopAllAsync();
		}
	}
}