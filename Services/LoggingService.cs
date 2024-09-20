using System;
using System.IO;
using System.Threading.Tasks;

namespace RestaurantManagementApp.Services
{
    public class LoggingService
    {
        private readonly string _logFilePath;

        public LoggingService()
        {
            // Get the file path where logs will be stored
            _logFilePath = Path.Combine(FileSystem.AppDataDirectory, "app_log.txt");
        }

        // Log an error message
        public async Task LogErrorAsync(string message, Exception ex = null)
        {
            var errorMessage = $"[{DateTime.Now}] ERROR: {message}";
            if (ex != null)
            {
                errorMessage += $"\nException: {ex.Message}\nStackTrace: {ex.StackTrace}";
            }

            await AppendLogAsync(errorMessage);
        }

        // Log a general message
        public async Task LogMessageAsync(string message)
        {
            var logMessage = $"[{DateTime.Now}] INFO: {message}";
            await AppendLogAsync(logMessage);
        }

        // Helper method to append logs to the file
        private async Task AppendLogAsync(string logMessage)
        {
            try
            {
                using (var writer = File.AppendText(_logFilePath))
                {
                    await writer.WriteLineAsync(logMessage);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to write to log file: {ex.Message}");
            }
        }

        // Retrieve the current log file path for other uses
        public string GetLogFilePath() => _logFilePath;
    }
}
