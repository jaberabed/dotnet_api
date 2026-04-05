namespace dotnet_articles_api.Logger
{
    public interface IAppLogger
    {
        void LogInfo(string message);

        void LogWarning(string message);

        void LogError(string message);
    }
}