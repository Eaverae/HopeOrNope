using HopeNope.iOS.Services;
using HopeNope.Services;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(StatusBarService))]
namespace HopeNope.iOS.Services
{

    /// <summary>
    /// Status bar service.
    /// </summary>
    /// <seealso cref="Rimek.Framework.App.Interfaces.IStatusBarService" />
    public class StatusBarService : IStatusBarService
    {
        /// <summary>
        /// Hides the status bar.
        /// </summary>
        public void HideStatusBar()
        {
            UIApplication.SharedApplication.StatusBarHidden = true;
        }

        /// <summary>
        /// Shows the status bar.
        /// </summary>
        public void ShowStatusBar()
        {
            UIApplication.SharedApplication.StatusBarHidden = false;
        }
    }
}