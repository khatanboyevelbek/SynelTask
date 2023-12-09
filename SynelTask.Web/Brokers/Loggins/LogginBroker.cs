namespace SynelTask.Web.Brokers.Loggins
{
    public class LogginBroker : ILogginBroker
    {
        private readonly ILogger logger;

        public LogginBroker(ILogger<LogginBroker> logger) =>
             this.logger = logger;

        public void LogCritical(Exception exception) =>
            this.logger.LogCritical(exception, exception.Message);

        public void LogDebug(string message) =>
            this.logger.LogDebug(message);

        public void LogError(string message) =>
            this.logger.LogError(message);

        public void LogInformation(string message) =>
            this.logger.LogInformation(message);

        public void LogTrace(string message) =>
            this.logger.LogTrace(message);

        public void LogWarning(string message) =>
            this.logger.LogWarning(message);
    }
}
