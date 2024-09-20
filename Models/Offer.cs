using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementApp.Models
{
    public partial class Offer : ObservableObject
    {
        public string? OfferHeading { get; set; }
        public string? OfferImage { get; set; }
        public string? OfferDesc { get; set; }
        public string? PerImage { get; set; }
        public string? CouponCode { get; set; }


    }
}
