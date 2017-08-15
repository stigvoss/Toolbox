using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toolbox.Logging.Args;
using Toolbox.Logging.Base;

namespace Toolbox.Logging
{
    public delegate void LoggingEventHandler(object sender, LoggingEventArgs args);

    public enum LOG_LEVEL
    {
        OFF,
        INFO,
        WARNING,
        ERROR,
        DEBUG,
        TRACE
    }

    public enum LOG_DETAIL
    {
        LOW,
        MEDIUM,
        HIGH
    }

    public class LoggingHandler
    {
        public event LoggingEventHandler Event;

        private static LoggingHandler _instance = null;

        public LOG_LEVEL Level { get; set; }

        public LOG_DETAIL Detail { get; set; }

        private Dictionary<Type, ILoggingProvider> _providers;

        public static LoggingHandler Instance
        {
            get
            {
                return _instance ?? (_instance = new LoggingHandler());
            }
        }

        private LoggingHandler()
        {
            _providers = new Dictionary<Type, ILoggingProvider>();

            Level = LOG_LEVEL.ERROR;
            Detail = LOG_DETAIL.HIGH;
        }

        public void AttachProvider<T>(IProviderInitializationArgs args = null) where T : ILoggingProvider, new()
        {
            if(args is null)
            {
                args = new DefaultInitalizationArgs();
            }

            if (!_providers.ContainsKey(typeof(T)))
            {
                T provider = new T();
                
                provider.Initialize(args);
                provider.Attach(this);

                _providers.Add(typeof(T), provider);
            }
        }

        public void DetachProvider<T>() where T : ILoggingProvider, new()
        {
            if (!_providers.ContainsKey(typeof(T))) return;

            ILoggingProvider provider = _providers[typeof(T)];

            provider.Detach(this);
        }

        public void Handle(object sender, LoggingEventArgs args)
        {
            if (args.Level <= Level)
            {
                args.Detail = Detail;
                args.TargetLevel = Level;
                Event?.Invoke(sender, args);
            }
        }

        public void Handle(
            object sender,
            string message,
            Exception exception,
            Guid uniqueIdentifier,
            LOG_LEVEL level = LOG_LEVEL.INFO)
        {
            Handle(sender, new LoggingEventArgs
            {
                Message = message,
                Exception = exception,
                Level = level,
                UniqueIdentifier = uniqueIdentifier
            });
        }

        public void Handle(
            object sender,
            Exception exception,
            Guid uniqueIdentifier,
            LOG_LEVEL level = LOG_LEVEL.INFO)
        {
            Handle(sender, exception.Message, exception, uniqueIdentifier, level);
        }

        public void Handle(
            object sender,
            Exception exception,
            LOG_LEVEL level = LOG_LEVEL.INFO)
        {
            Guid uniqueIdentifier = Guid.Empty;
            Handle(sender, exception.Message, exception, uniqueIdentifier, level);
        }

        public void Handle(
            object sender,
            string message,
            Guid uniqueIdentifier,
            LOG_LEVEL level = LOG_LEVEL.INFO)
        {
            Handle(sender, message, null, uniqueIdentifier, level);
        }

        public void Handle(
            object sender,
            string message,
            LOG_LEVEL level = LOG_LEVEL.INFO)
        {
            Guid uniqueIdentifier = Guid.Empty;
            Handle(sender, message, uniqueIdentifier, level);
        }

        public void Handle(
            object sender,
            string format,
            LOG_LEVEL level = LOG_LEVEL.INFO,
            params object[] args)
        {
            Handle(sender, string.Format(format, args), level);
        }
    }
}

