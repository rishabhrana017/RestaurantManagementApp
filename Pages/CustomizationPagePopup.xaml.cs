namespace RestaurantManagementApp.Pages;

public partial class CustomizationPagePopup : Popup
{
    private readonly CustomizationViewModel _customizationViewModel;
    public CustomizationPagePopup(CustomizationViewModel customizationViewModel)
    {
        InitializeComponent();
        _customizationViewModel = customizationViewModel;
        BindingContext = _customizationViewModel;
    }
}