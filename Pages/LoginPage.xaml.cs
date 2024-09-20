using RestaurantManagementApp.ViewModels;

namespace RestaurantManagementApp.Pages;

public partial class LoginPage : ContentPage
{
    private readonly LoginViewModel _loginViewModel;

    public LoginPage(LoginViewModel loginViewModel)
	{
		InitializeComponent();
        _loginViewModel = loginViewModel;
        BindingContext = _loginViewModel;
	}

    private void GoogleLogin_Clicked(object sender, EventArgs e)
    {

    }
}