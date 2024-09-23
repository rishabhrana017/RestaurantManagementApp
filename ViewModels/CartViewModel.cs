namespace RestaurantManagementApp.ViewModels
{
    public partial class CartViewModel : ObservableObject
    {
        public ObservableCollection<Item> Items { get; private set; } = new();

        private bool _isCouponApplied;

        [ObservableProperty]
        private double _totalAmount;

        

        [ObservableProperty]
        private double _distance;

        [ObservableProperty]
        private double _deliveryCharge;

        public double TotalAmountWithDelivery => TotalAmount + DeliveryCharge;

        public bool IsCollectionEmpty => Items.Count == 0;
        public bool IsCollectionNotEmpty => !IsCollectionEmpty;

        [ObservableProperty]
        private string _couponCode;

        public bool IsCouponApplied => !string.IsNullOrEmpty(CouponCode);

        public event EventHandler<Item> CartItemRemoved;
        public event EventHandler<Item> CartItemUpdated;
        public event EventHandler CartCleared;

        public CartViewModel()
        {
            Items.CollectionChanged += (s, e) => RecalculateTotalAmount();
        }

        [RelayCommand]
        public void UpdateCartItem(Item item)
        {
            var existingItem = Items.FirstOrDefault(i => i.Name == item.Name);
            if (existingItem != null)
            {
                existingItem.CartQuantity = item.CartQuantity;
                existingItem.CustomizationPriceTotal = item.CustomizationPriceTotal; // Include customization price
                CartItemUpdated?.Invoke(this, existingItem);
            }
            else
            {
                Items.Add(item.Clone());
                CartItemUpdated?.Invoke(this, item);
            }

            RecalculateTotalAmount(); // Recalculate the total whenever an item is updated
            OnPropertyChanged(nameof(IsCollectionEmpty));
            OnPropertyChanged(nameof(IsCollectionNotEmpty));
        }

        [RelayCommand]
        private async Task RemoveCartItemAsync(string name)
        {
            var item = Items.FirstOrDefault(i => i.Name == name);
            if (item != null)
            {
                Items.Remove(item);
                RecalculateTotalAmount();
                CartItemRemoved?.Invoke(this, item);

                var snackbarOptions = new SnackbarOptions
                {
                    CornerRadius = 10,
                    BackgroundColor = Colors.PaleGoldenrod
                };

                var snackbar = Snackbar.Make($"'{item.Name}' removed from cart",
                    () =>
                    {
                        Items.Add(item);
                        RecalculateTotalAmount();
                        CartItemUpdated?.Invoke(this, item);
                        OnPropertyChanged(nameof(IsCollectionEmpty));
                        OnPropertyChanged(nameof(IsCollectionNotEmpty));
                    }, "Undo", TimeSpan.FromSeconds(5), snackbarOptions);

                await snackbar.Show();
            }
        }

        [RelayCommand]
        private async Task ClearCartAsync()
        {
            bool confirm = await Shell.Current.DisplayAlert("Confirm Clear Cart", "Are you sure?", "Yes", "No");
            if (confirm)
            {
                Items.Clear();
                RecalculateTotalAmount();
                CartCleared?.Invoke(this, EventArgs.Empty);
                await Toast.Make("Cart Cleared", ToastDuration.Short).Show();
                OnPropertyChanged(nameof(IsCollectionEmpty));
                OnPropertyChanged(nameof(IsCollectionNotEmpty));
            }
        }

        [RelayCommand]
        private async Task PlaceOrderAsync()
        {
            await Shell.Current.GoToAsync(nameof(BillingPage), animate: true);
        }

        [RelayCommand]
        private async Task CheckoutAsync()
        {
            Items.Clear();
            RecalculateTotalAmount();
            CartCleared?.Invoke(this, EventArgs.Empty);
            await Shell.Current.GoToAsync(nameof(CheckoutPage), animate: true);
            OnPropertyChanged(nameof(IsCollectionEmpty));
            OnPropertyChanged(nameof(IsCollectionNotEmpty));
        }

        private void RecalculateTotalAmount()
        {
            // Updated to include the customization price in the total
            TotalAmount = Items.Sum(i => i.Amount + (i.CustomizationPriceTotal * i.CartQuantity));
            OnPropertyChanged(nameof(TotalAmountWithDelivery));
            OnPropertyChanged(nameof(TotalAmount));
        }

        public void UpdateDistance(double distance)
        {
            Distance = distance;
            DeliveryCharge = 4 * distance;
            OnPropertyChanged(nameof(TotalAmountWithDelivery));
        }

        [RelayCommand]
        private void ShowCouponPopup()
        {
            var popup = new CouponPopup(this);
            App.Current.MainPage.ShowPopup(popup);
        }

        [RelayCommand]
        private async Task ApplyCouponCodeAsync()
        {
            var validCouponCodes = new List<string> { "SUP10SUN", "MANIA20", "TASTY15", "WONDER25", "TRICKY10", "FANATIC30", "SIZZLE40" };
            if (_isCouponApplied)
            {
                await Toast.Make("Coupon already applied!", ToastDuration.Short).Show();
                return;
            }
            if (validCouponCodes.Contains(CouponCode))
            {
                double discountPercentage = 0;
                switch (DateTime.Now.DayOfWeek)
                {
                    case DayOfWeek.Monday:
                    case DayOfWeek.Tuesday:
                    case DayOfWeek.Wednesday:
                        discountPercentage = 0.20;
                        break;
                    case DayOfWeek.Thursday:
                    case DayOfWeek.Friday:
                        discountPercentage = 0.15;
                        break;
                    case DayOfWeek.Saturday:
                        discountPercentage = 0.10;
                        break;
                    case DayOfWeek.Sunday:
                        discountPercentage = 0.25;
                        break;
                }
                var discount = TotalAmount * discountPercentage;
                TotalAmount -= discount;
                await Toast.Make("Coupon Applied Successfully!", ToastDuration.Short).Show();
                _isCouponApplied = true;
                OnPropertyChanged(nameof(IsCouponApplied));
            }
            else
            {
                await Toast.Make("Invalid Coupon Code!", ToastDuration.Short).Show();
            }
        }
    }
}
