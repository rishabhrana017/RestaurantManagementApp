namespace RestaurantManagementApp.Models
{
   public partial class Customization : ObservableObject
    {
        public string? Name { get; set; }
        public string? Image { get; set; }
        public double Price { get; set; }
        [ObservableProperty]
        private bool _isButtonEnabled = true;
    }
}
