

namespace RestaurantManagementApp.ViewModels
{
    [QueryProperty(nameof(FromSearch), nameof(FromSearch))]
    public partial class AllItemViewModel : ObservableObject
    {
        private readonly ItemService _itemService;
        [ObservableProperty]
        private string _selectedCategory = "All";  // Default to "All"
        public AllItemViewModel(ItemService itemService)
        {
            _itemService = itemService;
            Items = new(_itemService.GetAllItems());
        }
        public ObservableCollection<Item> Items { get; set; }

        [ObservableProperty]
        private bool _fromSearch;

        [ObservableProperty]
        private bool _searching;

        [RelayCommand]
        private async Task SearchItems(string searchTerm)
        {
            Items.Clear();
            Searching = true;
            await Task.Delay(2000); // Simulate delay
            var filteredItems = _itemService.SearchItems(searchTerm);

            // Apply category filter
            filteredItems = _selectedCategory switch
            {
                "Veg" => filteredItems.Where(item => item.Type == "Veg"),
                "Non-Veg" => filteredItems.Where(item => item.Type == "Non-Veg"),
                _ => filteredItems
            };

            foreach (var item in filteredItems)
            {
                Items.Add(item);
            }
            Searching = false;
        }
        private void FilterItemsByCategory()
        {
            Items.Clear();
            var filteredItems = _selectedCategory switch
            {
                "Veg" => _itemService.GetVegItems(),
                "Non-Veg" => _itemService.GetNonVegItems(),
                _ => _itemService.GetAllItems()
            };
            foreach (var item in filteredItems)
            {
                Items.Add(item);
            }
        }

        [RelayCommand]
        public void ApplyCategoryFilter(string category)
        {
            SelectedCategory = category;
            FilterItemsByCategory();
        }


        [RelayCommand]
        private async Task GoToDetailsPage(Item item)
        {
            var parameters = new Dictionary<string, object>
            {
                [nameof(DetailsViewModel.Item)] = item
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), animate: true, parameters);
        }
    }
}
