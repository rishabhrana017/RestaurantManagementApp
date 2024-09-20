using Java.Util;
using RestaurantManagementApp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementApp.ViewModels
{

    public partial class OffersViewModel : ObservableObject
    {

        private readonly OfferService _offerService;
        public ObservableCollection<Offer> Offers { get; }
        public OffersViewModel(OfferService offerService)
        {
            _offerService = offerService;
            Offers = new ObservableCollection<Offer>(_offerService.GetAllOffers());

        }
        [RelayCommand]        
        private async Task CopyCouponCode(string couponCode)
        {
            if (!string.IsNullOrWhiteSpace(couponCode))
            {
                await Clipboard.SetTextAsync(couponCode);
                await Toast.Make("Sucessfully copied Coupon Code", ToastDuration.Short).Show();
            }
        }


    }
}
