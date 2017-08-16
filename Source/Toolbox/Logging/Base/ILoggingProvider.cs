namespace Toolbox.Logging.Base
{
    public interface ILoggingProvider
    {
        void Initialize(IProviderInitializationArgs args);

        void Attach(LoggingHandler handler);

        void Detach(LoggingHandler handler);
    }
}

