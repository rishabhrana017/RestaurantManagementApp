using RestaurantManagementApp.Models;
using RestaurantManagementApp.Services;
using Microsoft.Maui.Controls;
using System;
using System.IO;

namespace RestaurantManagementApp
{
    public partial class App : Application
    {
        // Static user information accessible throughout the app
        public static UserInfo UserInfo;

        private readonly LoggingService _loggingService;

        public App()
        {
            InitializeComponent();

            // Initialize the logging service for handling logs
            _loggingService = new LoggingService();

            // Set the main page of the application
            MainPage = new AppShell();

            // Perform Android-specific configurations
#if ANDROID
            ConfigureAndroidApp();
#endif

            // Subscribe to global unhandled exceptions
            SubscribeToUnhandledExceptions();
        }

        private void SubscribeToUnhandledExceptions()
        {
            // Handle exceptions on non-UI threads
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private async void OnUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception exception)
            {
                await _loggingService.LogErrorAsync("Unhandled Exception", exception);
            }
            else
            {
                await _loggingService.LogErrorAsync("Unhandled Non-Exception Error", null);
            }
        }

        private async void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e)
        {
            await _loggingService.LogErrorAsync("Unobserved Task Exception", e.Exception);
            e.SetObserved(); // Prevent the app from terminating
        }

#if ANDROID
        // Android-specific app configuration
        private void ConfigureAndroidApp()
        {
            // Example: Configure Android-specific permissions or services
            Console.WriteLine("Configuring Android-specific settings...");

            // Perform Android-specific startup tasks here
            // Example: Initialize Android-specific services or permissions
            InitializeAndroidServices();
        }

        private void InitializeAndroidServices()
        {
            // Initialize any services or configurations specific to Android platform
            // For example: Check permissions, setup background services, etc.
            Console.WriteLine("Android-specific services initialized.");
        }
#endif

        protected override void OnStart()
        {
            base.OnStart();
            _loggingService.LogMessageAsync("Application Started").ConfigureAwait(false);
        }

        protected override void OnSleep()
        {
            base.OnSleep();
            _loggingService.LogMessageAsync("Application Sleeping").ConfigureAwait(false);
        }

        protected override void OnResume()
        {
            base.OnResume();
            _loggingService.LogMessageAsync("Application Resumed").ConfigureAwait(false);
        }
    }
}
