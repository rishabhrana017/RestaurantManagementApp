using RestaurantManagementApp.ViewModels;

namespace RestaurantManagementApp.Pages;

public partial class CustomizationPage : ContentPage
{
    private readonly CustomizationViewModel _customizationViewModel;
    public CustomizationPage(CustomizationViewModel customizationViewModel)
    {
		InitializeComponent();
        _customizationViewModel = customizationViewModel;
        BindingContext = _customizationViewModel;
    }
}