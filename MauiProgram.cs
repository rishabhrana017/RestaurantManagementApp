using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using RestaurantManagementApp.Pages;
using RestaurantManagementApp.ViewModels;

namespace RestaurantManagementApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                }).UseMauiCommunityToolkit()
                .UseMauiMaps();

#if DEBUG
            AddItemServices(builder.Services);
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
        private static IServiceCollection
            AddItemServices(IServiceCollection services)
        {
            services.AddSingleton<ItemService>();
            services.AddSingleton<OfferService>();
            services.AddSingleton<CustomizationService>();


            services.AddSingleton<HomePage>()
                .AddSingleton<HomeViewModel>();
            services.AddTransientWithShellRoute<AllItemsPage,
                AllItemViewModel>(nameof(AllItemsPage));
            services.AddTransientWithShellRoute<DetailPage,
                DetailsViewModel>(nameof(DetailPage));
            services.AddTransientWithShellRoute<CartPage,
                CartViewModel>(nameof(CartPage));
            services.AddTransientWithShellRoute<BillingPage,
                CartViewModel>(nameof(BillingPage));
            services.AddSingleton<CartViewModel>();
            services.AddTransient<CartPage>();
            services.AddSingleton<LoginPage>();
            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<SplOfferPage>()
                .AddSingleton<OffersViewModel>();
            services.AddSingleton<CustomizationPage>();
            services.AddSingleton<CustomizationViewModel>();

            return services;
        }
    }
}
