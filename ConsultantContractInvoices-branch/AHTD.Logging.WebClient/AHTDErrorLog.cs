using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel;
using System.Text;

using Elmah;

using AHTD.Logging.AppLogService;

namespace AHTD.Logging
{
	/// <summary>
	/// An <see cref="ErrorLog"/> implementation that uses AHTD's AppLog as its
	/// backing store.
	/// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public class AHTDErrorLog : ErrorLog, IAppLoggingService
	{
		/// <summary>
		/// The key for configuring the Application Name via the default appSettings section.
		/// </summary>
		public const string AppNameAppSettingsKey = "AHTD_AppName";

		private readonly Type _applogServiceType;

		/// <summary>
		/// Initializes a new instance of the <see cref="AHTDErrorLog"/> class.
		/// </summary>
		/// <param name="config">A configuration settings dictionary.</param>
		/// <exception cref="T:System.ArgumentNullException">
		/// Thrown when <paramref name="config"/> is null.
		/// </exception>
		/// <exception cref="T:System.Configuration.ConfigurationErrorsException">
		/// Thrown when the application name cannot be determined.
		/// </exception>
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public AHTDErrorLog(IDictionary config)
		{
			if (config == null)
				throw new ArgumentNullException("config");

			_applogServiceType = typeof(AppLogServiceClient);

			// Get ApplicationName from settings or current virtual directory
			if (config["applicationName"] != null)
			{
				ApplicationName = (string)config["applicationName"];
			}
            else if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null && System.Web.HttpContext.Current.Request.ApplicationPath.Length > 1)
            {
                ApplicationName = System.Web.HttpContext.Current.Request.ApplicationPath.Replace("/", "");
            }
            else
            {
                ApplicationName = ConfigurationManager.AppSettings["applicationName"];
            }

			if (String.IsNullOrEmpty(ApplicationName))
				throw new ConfigurationErrorsException("Application name is missing for the AHTD error log.");
		}
		/// <summary>
		/// Initializes a new instance of the <see cref="AHTDErrorLog"/> class.
		/// </summary>
		/// <param name="applogServiceType">An AppLog service.</param>
		/// <exception cref="System.ArgumentNullException">
		/// Thrown when <paramref name="applogServiceType"/> is null.
		/// </exception>
		/// <exception cref="System.ArgumentException">
		/// Thrown when <paramref name="applogServiceType"/> does not implement
		/// ICommunicationObject or IAppLogService.
		/// </exception>
		/// <exception cref="T:System.InvalidOperationException">
		/// Thrown when the application name cannot be determined.
		/// </exception>
		public AHTDErrorLog(Type applogServiceType)
		{
			if (applogServiceType == null)
				throw new ArgumentNullException("applogServiceType");

			if (!(typeof(ICommunicationObject)).IsAssignableFrom(applogServiceType))
				throw new ArgumentException("Type must implement System.ServiceModel.ICommunicationObject", "applogServiceType");
			if (!(typeof(IAppLogService)).IsAssignableFrom(applogServiceType))
				throw new ArgumentException("Type must implement AHTD.Logging.AppLogService.IAppLogService", "applogServiceType");

			_applogServiceType = applogServiceType;

			if (System.Web.HttpContext.Current != null && System.Web.HttpContext.Current.Request != null)
			{
				ApplicationName = System.Web.HttpContext.Current.Request.ApplicationPath.Replace("/", "");
			}
			else
			{
				ApplicationName = ConfigurationManager.AppSettings[AppNameAppSettingsKey];

				if (ApplicationName == null)
				{
					throw new ConfigurationErrorsException(String.Format("No entry found in appSettings with key '{0}'.", AppNameAppSettingsKey));
				}
				else if (ApplicationName.Length == 0)
				{
					throw new ConfigurationErrorsException(String.Format("Cannot use empty value in appSettings for entry with key '{0}'.", AppNameAppSettingsKey));
				}
			}

			if (String.IsNullOrEmpty(ApplicationName))
				throw new InvalidOperationException("Application name cannot be determined. Consider adding AHTD_AppName to appSettings.");
		}
		/// <summary>
		/// Gets the name of this error log implementation.
		/// </summary>
		public override string Name
		{
			get { return "AHTD Error Log"; }
		}

		/// <summary>
		/// Retrieves a single application error from log given its
		/// identifier, or null if it does not exist.
		/// </summary>
		/// <param name="id"></param>
		/// <returns></returns>
		public override ErrorLogEntry GetError(string id)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Retrieves a page of application errors from the log in
		/// descending order of logged time.
		/// </summary>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <param name="errorEntryList"></param>
		/// <returns></returns>
		public override int GetErrors(int pageIndex, int pageSize, System.Collections.IList errorEntryList)
		{
			throw new NotImplementedException();
		}
		/// <summary>
		/// Logs an error in log for the application.
		/// </summary>
		/// <param name="error">An error to log.</param>
		/// <returns></returns>
		public override string Log(Error error)
		{
			LogEvent(EventType.UnhandledException, ElmahErrorFormatter.FormatElmahErrorMessage(error, true));

			return null;
		}
		/// <summary>
		/// Logs the message as the specified event type.
		/// </summary>
		/// <param name="eventType">Type of the event.</param>
		/// <param name="message">The message to log.</param>
		/// <remarks>
		/// The actual logging action happens asynchronously on a worker thread.
		/// If the log fails it will be saved into isolated storage and
		/// re-attempted later.
		/// </remarks>
		public void LogEvent(EventType eventType, string message)
		{
			ICommunicationObject client = (ICommunicationObject)Activator.CreateInstance(_applogServiceType);

			try
			{
				switch (eventType)
				{
					case EventType.Exception:
						((IAppLogService)client).LogException(ApplicationName, message);
						break;
					case EventType.UnhandledException:
						((IAppLogService)client).LogUnhandledException(ApplicationName, message);
						break;
					default:
					case EventType.Unspecified:
					case EventType.Trace:
						((IAppLogService)client).LogTraceMessage(ApplicationName, message);
						break;
					case EventType.Debug:
						((IAppLogService)client).LogDebugMessage(ApplicationName, message);
						break;
				}

				client.Close();
			}
			catch (CommunicationException e)
			{
				Debug.WriteLine("AHTDErrorLog : CommunicationException : " + e.Message);
				client.Abort();
			}
			catch (TimeoutException e)
			{
				Debug.WriteLine("AHTDErrorLog : TimeoutException : " + e.Message);
				client.Abort();
			}
			catch (Exception)
			{
				client.Abort();
				throw;
			}
		}

		/// <summary>
		/// Logs an exception's details.
		/// </summary>
		/// <param name="ex">An exception to log.</param>
		/// <param name="unhandled">if set to <c>true</c> exception was unhandled/fatal.</param>
		/// <param name="additionalInfo">A collection of key-value pairs to append to the Additional Info section.</param>
		void IAppLoggingService.LogException(Exception ex, bool unhandled, IEnumerable<KeyValuePair<string, string>> additionalInfo)
		{
			LogException(ex, unhandled, additionalInfo);
		}
		/// <summary>
		/// Logs a debug message.
		/// </summary>
		/// <param name="message">A message.</param>
		void IAppLoggingService.LogDebugMessage(string message)
		{
			LogDebugMessage(message);
		}
		/// <summary>
		/// Logs a trace message.
		/// </summary>
		/// <param name="message">A message.</param>
		void IAppLoggingService.LogTraceMessage(string message)
		{
			LogTraceMessage(message);
		}

		/// <summary>
		/// Logs an exception's details.
		/// </summary>
		/// <param name="ex">An exception to log.</param>
		/// <param name="unhandled">if set to <c>true</c> exception was unhandled/fatal.</param>
		/// <param name="additionalInfo">A collection of key-value pairs to append to the Additional Info section.</param>
		protected void LogException(Exception ex, bool unhandled, IEnumerable<KeyValuePair<string, string>> additionalInfo)
		{
			LogEvent(unhandled ? EventType.UnhandledException : EventType.Exception, ExceptionFormatter.FormatExceptionMessage(ex, unhandled, additionalInfo));
		}
		/// <summary>
		/// Logs a debug message.
		/// </summary>
		/// <param name="message">A message.</param>
		protected void LogDebugMessage(string message)
		{
			LogEvent(EventType.Debug, message);
		}
		/// <summary>
		/// Logs a trace message.
		/// </summary>
		/// <param name="message">A message.</param>
		protected void LogTraceMessage(string message)
		{
			LogEvent(EventType.Trace, message);
		}
	}
}
