using Tömasös.Models;

namespace Tömasös.ViewModels
{
    public class ViewCartViewModel
    {
        public int FullPrice { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        public int? FreeMeal { get; set; }
        public List<Dish> Cart { get; set; }

    }
}
