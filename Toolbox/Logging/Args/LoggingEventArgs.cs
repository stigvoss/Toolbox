using System;

namespace Toolbox.Logging.Args
{
    public class LoggingEventArgs
    {
        public string Message { get; set; }

        public Exception Exception { get; set; }

        public LOG_LEVEL Level { get; set; }

        public LOG_DETAIL Detail { get; set; }

        public LOG_LEVEL TargetLevel { get; set; }

        public Guid UniqueIdentifier { get; set; }
    }
}

