using RestaurantManagementApp.Pages;

namespace RestaurantManagementApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(SplOfferPage), typeof(SplOfferPage));
            Routing.RegisterRoute(nameof(CartPage), typeof(CartPage));
            Routing.RegisterRoute(nameof(CheckoutPage), typeof(CheckoutPage));
            Routing.RegisterRoute(nameof(BillingPage), typeof(BillingPage));
            Routing.RegisterRoute(nameof(CustomizationPage), typeof(CustomizationPage));

        }
    }
}
