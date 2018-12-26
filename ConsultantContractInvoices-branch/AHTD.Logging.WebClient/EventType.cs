using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AHTD.Logging
{
	/// <summary>
	/// Defines a list of valid loggable event types.
	/// </summary>
	public enum EventType
	{
		/// <summary>
		/// Infrastructure. This value should not be used by consumers.
		/// The value Trace will be substituted.
		/// </summary>
		Unspecified,
		/// <summary>
		/// A caught (but not necessarily handled) exception. Sometimes used
		/// for informational purposes but usually error handling.
		/// </summary>
		Exception,
		/// <summary>
		/// An unhandled exception, which implies an application crash.
		/// </summary>
		UnhandledException,
		/// <summary>
		/// A general message sent via System.Diagnostics.Trace. These are
		/// typically for non-sensitive instrumentation purposes.
		/// </summary>
		Trace,
		/// <summary>
		/// A general message sent via System.Diagnostics.Debug. These only
		/// occur in debug/test builds.
		/// </summary>
		Debug
	}
}
