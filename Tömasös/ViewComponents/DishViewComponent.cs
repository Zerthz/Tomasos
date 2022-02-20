using Microsoft.AspNetCore.Mvc;
using Tömasös.Models;

namespace Tömasös.ViewComponents
{
    public class DishViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<Product> ingredients)
        {
            string output = String.Join(", ", ingredients.Select(x=> x.ProductName));

            return View("DishIngredients", output);
        }
    }
}
