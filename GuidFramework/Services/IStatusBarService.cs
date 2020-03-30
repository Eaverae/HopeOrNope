namespace GuidFramework.Services
{
	/// <summary>
	/// Statusbar service
	/// </summary>
	public interface IStatusBarService
	{
		/// <summary>
		/// Hides the status bar.
		/// </summary>
		void HideStatusBar();

		/// <summary>
		/// Shows the status bar.
		/// </summary>
		void ShowStatusBar();
	}
}
