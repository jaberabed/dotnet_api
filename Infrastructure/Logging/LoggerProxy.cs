namespace dotnet_articles_api.Infrastructure.Logging
{
    public class LoggerProxy
    {
        private readonly ILogger<LoggerProxy> _logger;

        public LoggerProxy(ILogger<LoggerProxy> logger)
        {
            _logger = logger;
        }

        public void WriteLine(string message)
        {
            _logger.LogInformation(message); // info
        }

        public void WriteWarning(string message)
        {
            _logger.LogWarning(message); // warn
        }

        public void WriteError(string message)
        {
            _logger.LogError(message); // fail
        }
    }
}