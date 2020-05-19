using Java.Lang;
using static Java.Lang.Thread;

namespace GuidFramework.Android
{
	/// <summary>
	/// ExceptionHandler for any uncaught exception in the Android solution
	/// </summary>
	/// <seealso cref="Java.Lang.Object" />
	/// <seealso cref="Java.Lang.Thread.IUncaughtExceptionHandler" />
	public class UncaughtExceptionHandler : Object, IUncaughtExceptionHandler
	{
		/// <summary>
		/// Handles the uncaught exception.
		/// </summary>
		/// <param name="thread">The t.</param>
		/// <param name="throwable">The e.</param>
		public void UncaughtException(Thread thread, Throwable throwable)
		{
			if (throwable != null)
				GuidApp.OnUnhandledException(new Exception(throwable));
		}
	}
}