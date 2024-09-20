using CommunityToolkit.Mvvm.ComponentModel;

namespace RestaurantManagementApp.Models
{
    public partial class Item : ObservableObject
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        public string? Type { get; set; }
        public double CustomizationPrice { get; set; }


        [ObservableProperty, NotifyPropertyChangedFor(nameof(Amount)), NotifyPropertyChangedFor(nameof(AmountWithDiscount))]
        private int _cartQuantity;

        [ObservableProperty]
        private double _discountPercentage; // Property for discount percentage

        [ObservableProperty]
        private double _customizationPriceTotal; // Total price for customizations on this item

        public double DiscountedPrice => Price * (1 - (DiscountPercentage / 100)); // Calculate discounted price
        public double Amount => CartQuantity * Price; // Base amount without discount
        public double AmountWithDiscount => CartQuantity * DiscountedPrice; // Amount with discount applied
        public double TotalPriceWithCustomizations => (Price + CustomizationPriceTotal) * CartQuantity; // Total price with customizations

        public Item? Clone() => MemberwiseClone() as Item;
    }
}
