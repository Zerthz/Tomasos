using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Tömasös.Models;

namespace Tömasös.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(List<Dish> cart)
        {

            // cart är bara 0 om vi kommer ifrån en annan sida och går till meny sidan.
            if(cart.Count == 0)
            {
                try
                {
                    // har vi inte lagt till ngt i cart än kommer den här throwa exception och vi skapar en ny
                    cart = JsonSerializer.Deserialize<List<Dish>>(HttpContext.Session.GetString("Cart"));

                 }
                catch (Exception)
                {
                    cart = new List<Dish>();
                    
                }
            }
            return View("Cart", cart);
        }
    }
}
