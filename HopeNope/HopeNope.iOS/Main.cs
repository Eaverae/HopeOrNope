using UIKit;

namespace HopeNope.iOS
{
    /// <summary>
    /// Application class
    /// </summary>
    public class Application
    {
        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        static void Main(string[] args)
        {
            // if you want to use a different Application Delegate class from "AppDelegate"
            // you can specify it here.
            UIApplication.Main(args, null, "AppDelegate");           
        }
    }
}
