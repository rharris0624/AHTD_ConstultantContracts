using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using AHTD.Logging;

namespace ConsultantContractsInternal.Parts
{
    [Export(typeof(IAppLoggingService))]
    public sealed class AppLoggingService : IAppLoggingService
    {
        private readonly AHTDErrorLog _logger;

        private IAppLoggingService LoggingService
        {
            get { return (IAppLoggingService)_logger; }
        }

        public AppLoggingService()
        {
            var config = new Dictionary<string, string>();

            config["applicationName"] = "userprov";

            _logger = new AHTDErrorLog(config);
        }

        public void LogDebugMessage(string message)
        {
            LoggingService.LogDebugMessage(message);
        }
        public void LogException(Exception ex, bool unhandled = false, IEnumerable<KeyValuePair<string, string>> additionalInfo = null)
        {
            LoggingService.LogException(ex, unhandled, additionalInfo);
        }
        public void LogTraceMessage(string message)
        {
            LoggingService.LogTraceMessage(message);
        }
    }
}
