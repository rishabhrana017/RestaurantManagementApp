namespace RestaurantManagementApp.Pages;

public partial class MainPage : ContentPage
{
    int count = 0;

    public MainPage()
    {
        InitializeComponent();
    }

    async void OnGetStarted_Tapped(object sender, TappedEventArgs e)
    {
        await Shell.Current.GoToAsync("//HomePage");

    }
}
