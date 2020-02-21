using HopeNope.Interfaces;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;

namespace HopeNope.Handlers
{
	/// <summary>
	/// LogHandler
	/// </summary>
	/// <seealso cref="HopeNope.Interfaces.ILogHandler" />
	public class LogHandler : ILogHandler
	{
		/// <summary>
		/// Logs the exception.
		/// </summary>
		/// <param name="exception">The exception.</param>
		/// <param name="properties">Additional properties</param>
		public void LogException(Exception exception, IDictionary<string, string> properties = null)
		{
			if (exception != null)
			{
				try
				{
					Crashes.TrackError(exception, properties);
				}
				catch
				{
					// catch all exceptions
				}
			}
		}
	}
}
