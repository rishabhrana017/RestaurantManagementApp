namespace RestaurantManagementApp.Pages;

public partial class HomePage : ContentPage
{
    private readonly HomeViewModel _homeViewModel;
    public HomePage(HomeViewModel homeViewModel)
    {
        InitializeComponent();
        _homeViewModel = homeViewModel;
        BindingContext = _homeViewModel;
    }

    //private void OnCategoryChanged(object sender, CheckedChangedEventArgs e)
    //{
    //    // Get the ViewModel
    //    var viewModel = BindingContext as HomeViewModel;

    //    // Check which RadioButton was changed
    //    if (sender is RadioButton radioButton)
    //    {
    //        switch (radioButton.Content.ToString())
    //        {
    //            case "All":
    //                viewModel.IsAllChecked = radioButton.IsChecked;
    //                break;
    //            case "Veg":
    //                viewModel.IsVegChecked = radioButton.IsChecked;
    //                break;
    //            case "Non-Veg":
    //                viewModel.IsNonVegChecked = radioButton.IsChecked;
    //                break;
    //        }
    //    }
    //}
}