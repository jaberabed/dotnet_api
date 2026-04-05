namespace dotnet_articles_api.Logger
{
    public class FileLogger : IAppLogger
    {
        private readonly string _logFolderPath;

        public FileLogger()
        {
            // Creates a Logs folder inside your project at runtime
            _logFolderPath = Path.Combine(Directory.GetCurrentDirectory(), "Logs");

            // Create the folder if it doesn't exist
            if (!Directory.Exists(_logFolderPath))
                Directory.CreateDirectory(_logFolderPath);
        }

        private void WriteToFile(string level, string message)
        {
            // Creates a new file each day e.g: log-2026-04-05.txt
            var fileName = $"log-{DateTime.Now:yyyy-MM-dd}.txt";
            var filePath = Path.Combine(_logFolderPath, fileName);

            var logEntry = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] [{level}] {message}";

            // Append the log entry to the file
            File.AppendAllText(filePath, logEntry + Environment.NewLine);
        }

        public void LogInfo(string message)
        {
            WriteToFile("INFO", message);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"[INFO] {message}");
            Console.ResetColor();
        }

        public void LogWarning(string message)
        {
            WriteToFile("WARNING", message);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"[WARNING] {message}");
            Console.ResetColor();
        }

        public void LogError(string message)
        {
            WriteToFile("ERROR", message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[ERROR] {message}");
            Console.ResetColor();
        }
    }
}