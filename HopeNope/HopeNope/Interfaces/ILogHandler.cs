using System;
using System.Collections.Generic;

namespace HopeNope.Interfaces
{
	/// <summary>
	/// LogHandler interface
	/// </summary>
	public interface ILogHandler
	{
		/// <summary>
		/// Logs the exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="properties">Additional properties</param>
		void LogException(Exception exception, IDictionary<string, string> properties = null);
	}
}
