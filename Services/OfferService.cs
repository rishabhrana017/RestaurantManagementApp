namespace RestaurantManagementApp.Services
{
    public class OfferService
    {
        private readonly static IEnumerable<Offer> _offers = new
           List<Offer>()
        {
            new Offer()
            {
                OfferHeading = "SUPER SUNDAY",
                OfferImage ="sunday.png",
                OfferDesc = "Make your Sunday SUPER!!! with a limited-time discount! Use this code for exclusive 10% DISCOUNT and enjoy a relaxing day!",
                PerImage = "ten.png",
                CouponCode = "SUP10SUN"
            },
            new Offer()
            {
               OfferHeading = "MANIAC MONDAY",
                OfferImage ="monday.png",
                OfferDesc = "Beat the Monday blues with our MANIAC MONDAY offer! Use the below code for exclusive 20% DISCOUNT and start your week right!",
                PerImage = "twenty.png",
                CouponCode = "MANIA20"
            },
            new Offer()
            {
                OfferHeading = "TASTY TUESDAY",
                OfferImage ="tuesday.png",
                OfferDesc = "Add flavor to your week! Use the below code for a delicious 20% DISCOUNT and make your TUESDAY truly DELIGHTFUL!",
                PerImage = "twenty.png",
                CouponCode = "TASTY15"
            },
            new Offer()
            {
               OfferHeading = "WONDERFUL WEDNESDAY",
                OfferImage ="wednesday.png",
                OfferDesc = "Brighten your midweek with our WONDERFUL WEDNESDAY offer! Use the below code for a special 20% DISCOUNT and enjoy your day!",
                PerImage = "twenty.png",
                CouponCode = "WONDER25"
            },
            new Offer()
            {
                OfferHeading = "TRICKY THURSDAY",
                OfferImage ="thursday.png",
                OfferDesc = "Unlock a surprise with our TRICKY THURSDAY deal! Use the below code for a clever 15% DISCOUNT and spice up your day!",
                PerImage = "fifteen.png",
                CouponCode = "TRICKY10"
            },
            new Offer()
            {
                OfferHeading = "FANNATIC FRIDAY",
                OfferImage ="friday.png",
                OfferDesc = "Kick off the weekend with a FANATIC FRIDAY discount! Use the below code for amazing 15% DISCOUNT and start your weekend right!",
                PerImage = "fifteen.png",
                CouponCode = "FANATIC30"
            },
            new Offer()
            {
                OfferHeading = "SIZZELING SATURDAY",
                OfferImage ="saturday.png",
                OfferDesc = "Heat up your weekend with our SIZZLING SATURDAY offer! Use the below code for a hot 10% DISCOUNT and enjoy the day!",
                PerImage = "ten.png",
                CouponCode = "SIZZLE40"
            }
        };

        public IEnumerable<Offer> GetAllOffers()
        {
            var dayOfWeek = DateTime.Now.DayOfWeek;
            string dayName = dayOfWeek.ToString().ToUpper();

            var offer = _offers.FirstOrDefault(o => o.OfferHeading.ToUpper().Contains(dayName));

            if (offer != null)
            {
                yield return offer;
            }
        }
        //public IEnumerable<Offer> GetAllOffers(int count = 1) =>
        //     _offers.OrderBy(i => Guid.NewGuid()).Take(count);
    }
}
