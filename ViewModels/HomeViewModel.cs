using RestaurantManagementApp.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace RestaurantManagementApp.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly ItemService _itemService;

        // Properties for toggling between categories
        private bool _isAllChecked;
        private bool _isVegChecked;
        private bool _isNonVegChecked;

        // Filtered items collection
        public ObservableCollection<Item> FilteredItems { get; } = new();

        public HomeViewModel(ItemService itemService)
        {
            _itemService = itemService;
            Items = new ObservableCollection<Item>(_itemService.GetPopularItems());
        }
        [RelayCommand]
        public async void OnGoToOfferPage()
        {
            await Shell.Current.GoToAsync(nameof(SplOfferPage));
        }
        public ObservableCollection<Item> Items { get; }

        // Category selection properties
        

        // Command to navigate to the All Items page
        [RelayCommand]
        public async Task GoToAllItemsPage(bool fromSearch = false)
        {
            
            var parameters = new Dictionary<string, object>
            {
                [nameof(AllItemViewModel.FromSearch)] = fromSearch
            };
            await Shell.Current.GoToAsync(nameof(AllItemsPage), animate: true, parameters);
        }

        // Command to navigate to the Details page
        [RelayCommand]
        private async Task GoToDetailsPage(Item item)
        {
            var parameters = new Dictionary<string, object>
            {
                [nameof(DetailsViewModel.Item)] = item
            };
            await Shell.Current.GoToAsync(nameof(DetailPage), animate: true, parameters);
        }

        // Method to update the filtered items based on selected category
       
    }
}
