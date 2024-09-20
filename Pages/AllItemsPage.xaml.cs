namespace RestaurantManagementApp.Pages;

public partial class AllItemsPage : ContentPage
{
    private readonly AllItemViewModel _allItemViewModel;
    public AllItemsPage(AllItemViewModel allItemViewModel)
    {
        InitializeComponent();
        _allItemViewModel = allItemViewModel;
        BindingContext = _allItemViewModel;
    }
    void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        if (!string.IsNullOrWhiteSpace(e.OldTextValue) && string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            _allItemViewModel.SearchItemsCommand.Execute(null);
        }
    }
    void OnCategorySelected(object sender, EventArgs e)
    {
        if (sender is Picker picker)
        {
            var selectedCategory = picker.SelectedItem as string;
            if (selectedCategory != null)
            {
                _allItemViewModel.ApplyCategoryFilter(selectedCategory);
            }
        }
    }

}