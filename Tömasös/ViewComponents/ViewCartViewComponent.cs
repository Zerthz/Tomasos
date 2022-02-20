using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Tömasös.Models;
using Tömasös.ModelsIdentity;
using Tömasös.ViewModels;

namespace Tömasös.ViewComponents
{
    public class ViewCartViewComponent : ViewComponent
    {
        private readonly UserManager<ApplicationUser> _usermanager;

        public ViewCartViewComponent(UserManager<ApplicationUser> usermanager)
        {
            _usermanager = usermanager;
        }
        public async Task<IViewComponentResult> InvokeAsync(List<Dish> cart)
        {
            ViewCartViewModel model = new ViewCartViewModel();

            model.Cart = cart;
            // Pris på allt i cart
            model.Price = cart.Sum(x => x.Price);

            if(User.IsInRole("PremiumUser") || User.IsInRole("Admin"))
            {
                if(cart.Count >= 3)
                {
                    model.FullPrice = model.Price;
                    model.Discount = (int?)(model.Price * 0.2);
                    model.Price -= (int)model.Discount;
                }

                var user = await _usermanager.GetUserAsync(HttpContext.User);
                if(user.PremiumPoints >= 10)
                {
                    user.PremiumPoints = 0;

                    int minPrice = cart.Min(x => x.Price);
                    model.Price -= minPrice;
                    model.FreeMeal = minPrice;        

                }

                user.PremiumPoints += cart.Count();
                
                await _usermanager.UpdateAsync(user);
            }

            string orderJson = JsonSerializer.Serialize(model);
            HttpContext.Session.SetString("Order", orderJson);

            // är de premium? isf rabatt om det är 3 varor i cart
            // har de 10poäng? isf dra av billigaste rätten
            return View("/Views/Shared/Components/Cart/ViewCartPricing.cshtml", model);
        }
    }
}
