using System;
using Toolbox.Logging.Args;
using Toolbox.Logging.Base;

namespace Toolbox.Logging.Providers
{
    public class ConsoleLoggingProvider : LoggingProvider
    {
        protected override void Write(LoggingEventArgs args, object sender = null)
        {
            if (args.Detail == LOG_DETAIL.HIGH && args.TargetLevel == LOG_LEVEL.TRACE && sender != null)
            {
                Console.WriteLine("{1,-25}{2,-25}{3,-10}{0}", args.Message, InvokeTime, sender.GetType().Name, args.Level.ToString());
            }
            else
            {
                Console.WriteLine("{1,-25}{2,-10}{0}", args.Message, InvokeTime, args.Level.ToString());
            }

            if (args.Exception != null)
            {
                if (args.Detail == LOG_DETAIL.MEDIUM)
                {
                    Console.WriteLine("{1,-25}Exception Message: {0}", args.Exception.Message, InvokeTime);
                }
                else if (args.Detail == LOG_DETAIL.HIGH)
                {
                    Console.WriteLine("{1,-25}Exception Type: {0}", args.Exception.GetType().Name, InvokeTime);
                    Console.WriteLine("{1,-25}Exception Message: {0}", args.Exception.Message, InvokeTime);
                    Console.WriteLine("{1,-25}Exception Stack Trace:\n{0}", args.Exception.StackTrace, InvokeTime);
                }
            }
        }
    }
}

