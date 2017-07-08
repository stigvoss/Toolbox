using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Toolbox.Logging.Args;
using Toolbox.Logging.Base;

namespace Toolbox.Logging.Providers
{
    public class FileLoggingProvider : LoggingProvider
    {
        private const string LOG_FILE_NAME = "{0}.{1:HHmmss-ddMMyyyy}.log";
        private BlockingCollection<KeyValuePair<object, LoggingEventArgs>> _writeBuffer = new BlockingCollection<KeyValuePair<object, LoggingEventArgs>>();
        TaskFactory _factory = new TaskFactory();
        
        public override void Initialize(IProviderInitializationArgs args)
        {
            if(args is FileLoggingInitializationArgs)
            {

            }
            
            string logFileName = string.Format(LOG_FILE_NAME, DateTime.Now, "Default");
            _factory.StartNew(() => DoFileWriting(logFileName, _writeBuffer), TaskCreationOptions.LongRunning);
        }

        private void DoFileWriting(string logFileName, BlockingCollection<KeyValuePair<object, LoggingEventArgs>> writeBuffer)
        {
            foreach (var pair in writeBuffer.GetConsumingEnumerable())
            {
                using (StreamWriter writer = new StreamWriter(logFileName, true))
                {
                    Write(writer, pair.Value, pair.Key);
                }
            }
        }

        private void Write(StreamWriter writer, LoggingEventArgs args, object sender)
        {
            if (args.Detail == LOG_DETAIL.HIGH && args.TargetLevel == LOG_LEVEL.TRACE && sender != null)
            {
                writer.WriteLine("{1,-25}{3,-10}{2,-25}{0}", args.Message, InvokeTime, sender.GetType().Name, args.Level.ToString());
            }
            else
            {
                writer.WriteLine("{1,-25}{2,-10}{0}", args.Message, InvokeTime, args.Level.ToString());
            }

            if (args.Exception != null)
            {
                if (args.Detail == LOG_DETAIL.MEDIUM)
                {
                    writer.WriteLine("{1,-25}Exception Message: {0}", args.Exception.Message, InvokeTime);
                }
                else if (args.Detail == LOG_DETAIL.HIGH)
                {
                    writer.WriteLine("{1,-25}Exception Type: {0}", args.Exception.GetType().Name, InvokeTime);
                    writer.WriteLine("{1,-25}Exception Message: {0}", args.Exception.Message, InvokeTime);
                    writer.WriteLine("{1,-25}Exception Stack Trace:\n{0}", args.Exception.StackTrace, InvokeTime);
                }
            }
        }

        protected override void Write(LoggingEventArgs args, object sender = null)
        {
            var pair = new KeyValuePair<object, LoggingEventArgs>(sender, args);
            _writeBuffer.Add(pair);
        }
    }
}

