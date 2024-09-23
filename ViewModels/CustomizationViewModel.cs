namespace RestaurantManagementApp.ViewModels
{
    public partial class CustomizationViewModel : ObservableObject
    {
        private readonly CustomizationService _customizationService;

        public event EventHandler<CustomizationEventArgs>? CustomizationAdded;

        public ObservableCollection<Customization> Customization { get; }

        public CustomizationViewModel(CustomizationService customizationService)
        {
            _customizationService = customizationService;
            Customization = new ObservableCollection<Customization>(_customizationService.GetAllCustomizableItems());
        }

        [ObservableProperty]
        private bool _isAddCustomizationButtonEnabled = true;

        [RelayCommand]
        private async void AddCustomization(Customization customization)
        {
            if (customization.IsButtonEnabled)
            {
                customization.IsButtonEnabled = false;

                CustomizationAdded?.Invoke(this, new CustomizationEventArgs(customization.Price));

                await Toast.Make($"{customization.Name} customization added.", ToastDuration.Short).Show();
            }
            else
            {
                await Toast.Make("Customization already added.", ToastDuration.Short).Show();
            }
        }
    }

    public class CustomizationEventArgs : EventArgs
    {
        public double CustomizationPrice { get; }

        public CustomizationEventArgs(double customizationPrice)
        {
            CustomizationPrice = customizationPrice;
        }
    }
}
