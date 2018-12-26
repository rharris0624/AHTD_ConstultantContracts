using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text;

namespace AHTD.Logging
{
	[InheritedExport]
	public interface IAppLoggingService
	{
		/// <summary>
		/// Logs an exception's details.
		/// </summary>
		/// <param name="ex">An exception to log.</param>
		/// <param name="unhandled">if set to <c>true</c> exception was unhandled/fatal.</param>
		/// <param name="additionalInfo">A collection of key-value pairs to append to the Additional Info section.</param>
		void LogException(Exception ex, bool unhandled = false, IEnumerable<KeyValuePair<string, string>> additionalInfo = null);
		/// <summary>
		/// Logs a debug message.
		/// </summary>
		/// <param name="message">A message.</param>
		void LogDebugMessage(string message);
		/// <summary>
		/// Logs a trace message.
		/// </summary>
		/// <param name="message">A message.</param>
		void LogTraceMessage(string message);
	}
}
