namespace RestaurantManagementApp.Pages;


    public partial class CouponPopup : CommunityToolkit.Maui.Views.Popup
	{
    private readonly CartViewModel _cartViewModel;

    public CouponPopup(CartViewModel cartViewModel)
	{
        _cartViewModel = cartViewModel;

        InitializeComponent();
        BindingContext = _cartViewModel;

    }
}
