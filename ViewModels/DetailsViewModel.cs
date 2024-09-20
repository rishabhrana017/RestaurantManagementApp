using CommunityToolkit.Maui.Views;
using RestaurantManagementApp.Pages;
using RestaurantManagementApp.Services;
using System;
using System.Threading.Tasks;

namespace RestaurantManagementApp.ViewModels
{
    [QueryProperty(nameof(Item), nameof(Item))]
    public partial class DetailsViewModel : ObservableObject, IDisposable
    {
        private readonly CartViewModel _cartViewModel;
        private readonly CustomizationService _customizationService; // Inject CustomizationService

        public DetailsViewModel(CartViewModel cartViewModel, CustomizationService customizationService)
        {
            _cartViewModel = cartViewModel;
            _customizationService = customizationService;

            _cartViewModel.CartCleared += OnCartCleared;
            _cartViewModel.CartItemUpdated += OnCartItemUpdated;
            _cartViewModel.CartItemRemoved += OnCartItemRemoved;

            TotalAmount = (decimal)(Item?.TotalPriceWithCustomizations ?? 0);
        }

        [ObservableProperty]
        private Item? _item;

        [ObservableProperty]
        private decimal _totalAmount;

        [RelayCommand]
        private void AddToCart()
        {
            Item.CartQuantity++;
            UpdateTotalAmount();
            _cartViewModel.UpdateCartItemCommand.Execute(Item);
        }

        [RelayCommand]
        private void RemoveFromCart()
        {
            if (Item.CartQuantity > 0)
            {
                Item.CartQuantity--;
                UpdateTotalAmount();
                _cartViewModel.UpdateCartItemCommand.Execute(Item);
            }
        }

        [RelayCommand]
        private void ShowCustomizationPopup()
        {
            var customizationViewModel = new CustomizationViewModel(_customizationService);
            customizationViewModel.CustomizationAdded += OnCustomizationAdded; // Subscribe to event

            var popup = new CustomizationPagePopup(customizationViewModel);
            Shell.Current.CurrentPage.ShowPopup(popup);
        }

        private void OnCustomizationAdded(object? sender, CustomizationEventArgs e)
        {
            AddCustomizationPrice(e.CustomizationPrice);
        }

        public void AddCustomizationPrice(double customizationPrice)
        {
            if (Item != null)
            {
                Item.CustomizationPriceTotal += customizationPrice; // Add customization price to the item
                UpdateTotalAmount(); // Update the total price with customizations
            }
        }

        private void UpdateTotalAmount()
        {
            TotalAmount = (decimal)(Item?.TotalPriceWithCustomizations ?? 0);
        }

        [RelayCommand]
        private async Task ViewCart()
        {
            if (Item.CartQuantity > 0)
            {
                await Shell.Current.GoToAsync(nameof(CartPage), animate: true);
            }
            else
            {
                await Toast.Make("Please select the quantity using the plus(+) button", ToastDuration.Short).Show();
            }
        }

        partial void OnItemChanged(Item value)
        {
            if (value != null)
            {
                TotalAmount = (decimal)value.TotalPriceWithCustomizations;
                value.CustomizationPriceTotal = 0; // Reset customizations for the new item
                ResetCustomizationButtons();
            }
        }

        private void ResetCustomizationButtons()
        {
            var customizationViewModel = new CustomizationViewModel(_customizationService);
            foreach (var customization in customizationViewModel.Customization)
            {
                customization.IsButtonEnabled = true; // Enable all customization buttons
            }
        }

        public void Dispose()
        {
            _cartViewModel.CartCleared -= OnCartCleared;
            _cartViewModel.CartItemUpdated -= OnCartItemUpdated;
            _cartViewModel.CartItemRemoved -= OnCartItemRemoved;
        }

        public void OnCartCleared(object? _, EventArgs e) => Item.CartQuantity = 0;
        public void OnCartItemRemoved(object? _, Item i) => OnCartItemChanged(i, 0);
        public void OnCartItemUpdated(object? _, Item i) => OnCartItemChanged(i, i.CartQuantity);

        public void OnCartItemChanged(Item i, int quantity)
        {
            if (i.Name == Item?.Name)
            {
                Item.CartQuantity = quantity;
            }
        }
    }
}
