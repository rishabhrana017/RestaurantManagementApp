namespace RestaurantManagementApp.Pages;

public partial class SplOfferPage : ContentPage
{
    private readonly OffersViewModel _offersViewModel;
    public SplOfferPage (OffersViewModel offersViewModel)
    {
        InitializeComponent();
        _offersViewModel = offersViewModel;
        BindingContext = _offersViewModel;
    }
}