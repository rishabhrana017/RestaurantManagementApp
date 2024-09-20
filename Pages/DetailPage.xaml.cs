namespace RestaurantManagementApp.Pages;

public partial class DetailPage : ContentPage
{
    private readonly DetailsViewModel _detailsViewModel;
    public DetailPage(DetailsViewModel detailsViewModel)
    {
        _detailsViewModel = detailsViewModel;
        InitializeComponent();
        BindingContext = _detailsViewModel;
    }

    async void ImageButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
    protected override void OnNavigatingFrom(NavigatingFromEventArgs args)
    {
        base.OnNavigatingFrom(args);
        Behaviors.Add(new
            CommunityToolkit.Maui.Behaviors.StatusBarBehavior
        {
            StatusBarColor = Colors.DarkGoldenrod,
            StatusBarStyle = CommunityToolkit.Maui.Core.StatusBarStyle.DarkContent
        });
    }
}