using System;
using Toolbox.Logging.Args;

namespace Toolbox.Logging.Base
{
    public abstract class LoggingProvider : ILoggingProvider
    {
        protected DateTime InvokeTime;

        public void Attach(LoggingHandler handler)
        {
            handler.Event += OnEvent;
        }

        public void Detach(LoggingHandler handler)
        {
            handler.Event -= OnEvent;
        }

        protected void OnEvent(object sender, LoggingEventArgs args)
        {
            InvokeTime = DateTime.Now;

            switch (args.TargetLevel)
            {
                case LOG_LEVEL.INFO:
                    OnInfo(sender, args);
                    break;
                case LOG_LEVEL.WARNING:
                    OnWarning(sender, args);
                    break;
                case LOG_LEVEL.ERROR:
                    OnError(sender, args);
                    break;
                case LOG_LEVEL.DEBUG:
                    OnDebug(sender, args);
                    break;
                case LOG_LEVEL.TRACE:
                    OnTrace(sender, args);
                    break;
                default:
                    break;
            }
        }

        protected void OnInfo(object sender, LoggingEventArgs args)
        {
            if ((int)args.Level <= (int)LOG_LEVEL.INFO)
            {
                Write(args, sender);
            }
        }

        protected void OnWarning(object sender, LoggingEventArgs args)
        {
            if ((int)args.Level <= (int)LOG_LEVEL.WARNING)
            {
                Write(args, sender);
            }
        }

        protected void OnError(object sender, LoggingEventArgs args)
        {
            if ((int)args.Level <= (int)LOG_LEVEL.ERROR)
            {
                Write(args, sender);
            }
        }

        protected void OnDebug(object sender, LoggingEventArgs args)
        {
            if (args.Detail < LOG_DETAIL.MEDIUM)
            {
                args.Detail = LOG_DETAIL.MEDIUM;
            }
            if ((int)args.Level <= (int)LOG_LEVEL.DEBUG)
            {
                Write(args, sender);
            }
        }

        protected void OnTrace(object sender, LoggingEventArgs args)
        {
            if ((int)args.Detail < (int)LOG_DETAIL.HIGH)
            {
                args.Detail = LOG_DETAIL.HIGH;
            }
            if ((int)args.Level <= (int)LOG_LEVEL.TRACE)
            {
                Write(args, sender);
            }
        }

        public virtual void Initialize(IProviderInitializationArgs args) { }

        protected abstract void Write(LoggingEventArgs args, object sender);
    }
}

