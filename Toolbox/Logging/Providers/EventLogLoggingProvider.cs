using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Toolbox.Logging.Args;
using Toolbox.Logging.Base;
using Toolbox.Logging.Exceptions;

namespace Toolbox.Logging.Providers
{
    public class EventLogLoggingProvider : LoggingProvider
    {
        EventLog _logger;

        public override void Initialize(IProviderInitializationArgs args)
        {
            _logger = new EventLog();

            if (args is EventLogInitializationArgs)
            {
                EventLogInitializationArgs initArgs = (EventLogInitializationArgs)args;

                _logger.Source = initArgs.Source;
                _logger.Log = initArgs.Log;
            }
            else
            {
                throw new UnsupportedProviderInitializationArgs("Provider initlization args are not of a supported type");
            }

        }

        protected override void Write(LoggingEventArgs args, object sender)
        {
            string entry = "";

            entry += string.Format("{1,-25}Unique ID: {0}\n", args.UniqueIdentifier, InvokeTime);

            if (args.Detail == LOG_DETAIL.HIGH && args.TargetLevel == LOG_LEVEL.TRACE && sender != null)
            {
                entry += string.Format("{1,-25}{3,-10}{2,-25}{0}\n", args.Message, InvokeTime, sender?.GetType().Name, args.Level.ToString());
            }
            else
            {
                entry += string.Format("{1,-25}{2,-10}{0}\n", args.Message, InvokeTime, args.Level.ToString());
            }

            if (args.Exception != null)
            {
                if (args.Detail == LOG_DETAIL.MEDIUM)
                {
                    entry += string.Format("{1,-25}Exception Message: {0}", args.Exception.Message, InvokeTime);
                }
                else if (args.Detail == LOG_DETAIL.HIGH)
                {
                    entry += string.Format("{1,-25}Exception Type: {0}\n", args.Exception.GetType().Name, InvokeTime);
                    entry += string.Format("{1,-25}Exception Message: {0}\n", args.Exception.Message, InvokeTime);
                    entry += string.Format("{1,-25}Exception Stack Trace:\n{0}", args.Exception.StackTrace, InvokeTime);
                }
            }

            EventLogEntryType type;

            switch(args.Level)
            {
                case LOG_LEVEL.ERROR:
                    type = EventLogEntryType.Error;
                    break;
                case LOG_LEVEL.WARNING:
                    type = EventLogEntryType.Warning;
                    break;
                default:
                    type = EventLogEntryType.Information;
                    break;
            }

            _logger.WriteEntry(entry, type);
        }
    }
}
