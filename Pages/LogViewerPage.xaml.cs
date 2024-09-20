using Android.Content;
using RestaurantManagementApp.Services;
using System.IO;
using System.Threading.Tasks;

namespace RestaurantManagementApp.Pages
{
    public partial class LogViewerPage : ContentPage
    {
        private readonly LoggingService _loggingService;

        public LogViewerPage(LoggingService loggingService)
        {
            InitializeComponent();
            _loggingService = loggingService;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            // Load the log file and display it
            var logContent = await LoadLogFileAsync();
            LogText.Text = logContent;
        }

        private async Task<string> LoadLogFileAsync()
        {
            var logFilePath = _loggingService.GetLogFilePath();
            if (File.Exists(logFilePath))
            {
                return await File.ReadAllTextAsync(logFilePath);
            }
            return "No logs found.";
        }
    }
}
