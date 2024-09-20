using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantManagementApp.Services
{
    public class CustomizationService
    {
        private readonly static IEnumerable<Customization> _customization = new
            List<Customization>()
        {
            new Customization ()
            {
                Name = "Onion",
                Image = "onion_slice.png",
                Price = 15
            },
            new Customization ()
            {
                Name = "Tomato",
                Image = "tomato_slice.png",
                Price = 15
            },
            new Customization ()
            {
                Name = "Lettuce",
                Image = "lettuce.png",
                Price = 20
            },
            new Customization ()
            {
                Name = "Mozzarella Cheese",
                Image = "mozzarella.png",
                Price = 30
            },
            new Customization ()
            {
                Name = "Cheddar Cheese",
                Image = "cheese.png",
                Price = 30
            },
            new Customization ()
            {
                Name = "Pickles",
                Image = "pickle.png",
                Price = 15
            },
            new Customization ()
            {
                Name = "Avocado",
                Image = "avocado.png",
                Price = 30
            },
            new Customization ()
            {
                Name = "Mushrooms",
                Image = "mushroom.png",
                Price = 25
            },
            new Customization ()
            {
                Name = "Jalapeños",
                Image = "jalapenos.png",
                Price = 25
            },
            
            
            new Customization ()
            {
                Name = "Pineapple",
                Image = "pineapple.png",
                Price = 20
            },
            new Customization ()
            {
                Name = "Veg Patty",
                Image = "patty.png",
                Price = 25
            },
            new Customization ()
            {
                Name = "Paneer",
                Image = "paneer.png",
                Price = 30
            },
            new Customization ()
            {
                Name = "Capsicum",
                Image = "capsicum.png",
                Price = 15
            },
            new Customization ()
            {
                Name = "Black Olive",
                Image = "black_olives.png",
                Price = 25
            },
            new Customization ()
            {
                Name = "Green Olive",
                Image = "green_olives.png",
                Price = 25
            }
        };
        
        public IEnumerable<Customization> GetAllCustomizableItems() => _customization;
    }
}
