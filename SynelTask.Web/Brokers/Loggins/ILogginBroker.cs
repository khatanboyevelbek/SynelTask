namespace SynelTask.Web.Brokers.Loggins
{
    public interface ILogginBroker
    {
        void LogInformation(string message);
        void LogDebug(string message);
        void LogError(string message);
        void LogWarning(string message);
        void LogTrace(string message);
        void LogCritical(Exception exception);
    }
}
