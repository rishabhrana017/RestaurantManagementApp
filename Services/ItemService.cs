
namespace RestaurantManagementApp.Services
{
    public class ItemService
    {
        private readonly static IEnumerable<Item> _items = new
            List<Item>()
        {
            new Item()
            {
                Name = "Veg Loaded Pizza",
                Image ="onion.png",
                Price = 120,
                Type = "Veg"
            },
            new Item()
            {
                Name = "Farm House Pizza",
                Image ="farm_house.png",
                Price = 190,
                Type = "Non-Veg"
            },
            new Item()
            {
                Name = "Veg Burrito",
                Image ="burrito.png",
                Price = 80,
                Type = "Veg"
            },
            new Item()
            {
                Name = "Veg Burger",
                Image ="veg_burger.png",
                Price = 70,
                Type = "Veg"
            },
            new Item()
            {
                Name = "Ham Burger",
                Image ="hamburger.png",
                Price = 100,
                Type = "Non-Veg"
            },
            new Item()
            {
                Name = "King Burger",
                Image ="king_size.png",
                Price = 130,
                Type = "Non-Veg"
            },
            new Item()
            {
                Name = "Veg Sub",
                Image ="sub.png",
                Price = 120,
                Type = "Veg"
            },
            new Item()
            {
                Name = "Sandwich",
                Image ="sandwich.png",
                Price = 50,
                Type = "Veg"
            },
            new Item()
            {
                Name = "Grilled Sandwich",
                Image ="grilled_sandwich",
                Price = 80,
                Type = "Veg"
            },
            new Item()
            {
                Name = "Coffee",
                Image ="coffee.png",
                Price = 40,
                Type = "Veg"
            },
            new Item()
            {
                Name = "Cold Coffee",
                Image ="cold_coffee.png",
                Price = 70,
                Type = "Veg"
            },
            new Item()
            {
                Name = "Smoothie",
                Image ="smoothie.png",
                Price = 120,
                Type = "Veg"
            }
        };

        

        public IEnumerable<Item> GetAllItems() => _items;

        public IEnumerable<Item> GetPopularItems(int count = 6) =>
            _items.OrderBy(i => Guid.NewGuid()).Take(count);

        public IEnumerable<Item> SearchItems(string SreachTerm) =>
            string.IsNullOrWhiteSpace(SreachTerm)
            ? _items
            : _items.Where(i => i.Name.Contains(SreachTerm,
                StringComparison.OrdinalIgnoreCase));
        public IEnumerable<Item> GetVegItems() =>
        _items.Where(i => i.Type == "Veg");

        public IEnumerable<Item> GetNonVegItems() =>
            _items.Where(i => i.Type == "Non-Veg");
    }
}
