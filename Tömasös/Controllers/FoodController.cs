using Microsoft.AspNetCore.Mvc;
using Tömasös.Models;
using System.Text.Json;
using Tömasös.Repository;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Tömasös.ModelsIdentity;
using Tömasös.ViewModels;

namespace Tömasös.Controllers
{
    public class FoodController : Controller
    {
        private readonly IDishRepo _dishRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly UserManager<ApplicationUser> _userManager;

        public FoodController(IDishRepo dishRepo, IOrderRepo orderRepo, UserManager<ApplicationUser> userManager)
        {
            _dishRepo = dishRepo;
            _orderRepo = orderRepo;
            _userManager = userManager;
        }

        public IActionResult AddToCart(int dishId)
        {

            List<Dish> cart = GetCart();
            
              
            cart.Add(_dishRepo.GetDishById(dishId));

            SaveCart(cart);
            

            return ViewComponent("Cart", cart);
        }

        public IActionResult ViewCart()
        {
            List<Dish> cart = GetCart();
            return View(cart);
        }

        [Authorize]
        public IActionResult CheckOut()
        {
            // Töm cart från beställningen
            string emptyCartJson = JsonSerializer.Serialize(new List<Dish>());
            HttpContext.Session.Remove("Cart");

            ViewCartViewModel model;
            try
            {
                model = JsonSerializer.Deserialize<ViewCartViewModel>(HttpContext.Session.GetString("Order")!)!;
            }
            catch (Exception)
            {
                model = new ViewCartViewModel();
            }

            HttpContext.Session.Remove("Order");

            // Lägg till i Databasen av orders
            Order newOrder = new Order();
            newOrder.CustomerId = _userManager.GetUserId(User);
            newOrder.OrderDate = DateTime.Now;
            newOrder.TotalPrice = model.Price;
            newOrder.IsDelivered = false;
            int orderId = _orderRepo.InsertOrder(newOrder);

            List<OrderDish> orderDishes = new List<OrderDish>();
            var groupedCart = model.Cart.GroupBy(x => x.DishName);
            foreach (var dish in groupedCart)
            {
                OrderDish order = new OrderDish();
                order.Amount = dish.Count();
                order.DishId = dish.First().DishId;
                order.OrderId = orderId;

                orderDishes.Add(order);

            }
            _orderRepo.InsertOrderDishes(orderDishes);
            return View("CheckOut");
        }

        public IActionResult RemoveFromCart(int id)
        {
            var cart = GetCart();
            var dishToRemove = cart.First(x => x.DishId == id);
            cart.Remove(dishToRemove);

            SaveCart(cart);

            return RedirectToAction("ViewCart", "Food");
        }
        
        




        // De här 2 borde kanske vara services men är inte hundra hur jag får HttpContext där. tror inte man bör försöka heller?
        
        private void SaveCart(List<Dish> cart)
        {
            JsonSerializerOptions jsonOptions = new JsonSerializerOptions();
            jsonOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

            string serializedCart = JsonSerializer.Serialize(cart, jsonOptions);
            HttpContext.Session.SetString("Cart", serializedCart);

        }


        private List<Dish> GetCart()
        {
            List<Dish> cart;
            // kolla om vi har en cart. och i så fall hämta den
            if (HttpContext.Session.GetString("Cart") is null)
            {
                cart = new List<Dish>();
            }
            else
            {
                string cartJson = HttpContext.Session.GetString("Cart")!;
                cart = JsonSerializer.Deserialize<List<Dish>>(cartJson)!;
            }

            return cart;
        }
    }
}
