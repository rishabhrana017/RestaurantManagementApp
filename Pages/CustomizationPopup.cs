using CommunityToolkit.Maui.Views;
using RestaurantManagementApp.ViewModels;

namespace RestaurantManagementApp.Pages
{
    public class CustomizationPopup : Popup
    {
        public CustomizationPopup(CustomizationViewModel customizationViewModel)
        {
            // Set dimensions for the popup (optional)
            this.Size = new Size(300, 400); // Adjust the size as per your need

            // Embed CustomizationPage inside the popup with the ViewModel
//            Content = new CustomizationPage(customizationViewModel);
        }
    }
}
