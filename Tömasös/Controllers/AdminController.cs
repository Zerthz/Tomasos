using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tömasös.Models;
using Tömasös.ModelsIdentity;
using Tömasös.Repository;
using Tömasös.Services;
using Tömasös.ViewModels.Admin;

namespace Tömasös.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IDishRepo _dishRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IDishService _dishService;
        private readonly IAdminService _adminService;

        public AdminController(ICustomerRepo customerRepo, IDishRepo dishRepo, IOrderRepo orderRepo, 
            UserManager<ApplicationUser> userManager, IDishService dishService,
            IAdminService adminService)
        {
            _customerRepo = customerRepo;
            _dishRepo = dishRepo;
            _orderRepo = orderRepo;
            _userManager = userManager;
            _dishService = dishService;
            _adminService = adminService;
        }

        public IActionResult Dashboard()
        {
            var model = _adminService.GetAdminViewModel();

            return View(model);
        }

        public IActionResult ViewCustomer(string id)
        {
            var model = _adminService.GetUserViewModel(id);
           
            return ViewComponent("AdminCustomers", model);
        }
        public IActionResult ViewOrder(int id)
        {
            var model = _adminService.GetOrderViewModel(id);
            return ViewComponent("AdminOrders", model);
        }

        public IActionResult RemoveOrder(int id)
        {
            _orderRepo.RemoveOrder(id);

            return RedirectToAction("Dashboard");

        }

        // TODO: Move ingredient generation out to a service
        public IActionResult ViewDish(int id)
        {
            var model = _adminService.GetDishViewModel(id);

            return ViewComponent("AdminDish", model);
        }

        public IActionResult CreateNewDish()
        {
            var model = _adminService.GetEmptyDishViewModel();            

            return ViewComponent("AdminCreateDish", model);
        }

        public IActionResult CreateNewIngredient()
        {
            return ViewComponent("AdminCreateIngredient");
        }

        [HttpPost]
        public IActionResult CreateNewIngredient(Product product)
        {
            if (ModelState.IsValid)
            {
                //spara ingrediensen till db
                _dishRepo.CreateProduct(product);

                return RedirectToAction("Dashboard");
            }
            else
            {
                var model = _adminService.GetAdminViewModel();

                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

                ViewBag.Errors = String.Join(", ", allErrors.Select(x => x.ErrorMessage));

                return View("Dashboard", model);

            }
        }

        [HttpPost]
        public IActionResult CreateNewDish(AdminDishViewModel model)
        {
            if(string.IsNullOrEmpty(model.DishEditedCategoryName) == false)
            {
                model.Dish.DishTypeNavigation = _dishRepo.GetDishTypeByName(model.DishEditedCategoryName);
                model.Dish.DishType = model.Dish.DishTypeNavigation.DishType1;
                ModelState.Clear();
                TryValidateModel(model);
            }

            foreach (var item in ModelState.Keys)
            {
                if (item.Contains("CurrentDish"))
                {

                } 
                else
                {
                    ModelState[item].Errors.Clear();
                    ModelState[item].ValidationState = ModelValidationState.Valid;
                }
            }
            if (ModelState.IsValid)
            {
                model.Dish.Products = _dishRepo.GetProductsByName(model.DishEditedIngredients.ToList());

                // Spara rätten 
                _dishRepo.CreateDish(model.Dish);

                return RedirectToAction("Dashboard");


            }
            else
            {
                AdminViewModel adminmodel = _adminService.GetAdminViewModel();


                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

                ViewBag.Errors = String.Join(", ", allErrors.Select(x => x.ErrorMessage));
                return View("Dashboard", adminmodel);
            }



        }

        [HttpPost]
        public IActionResult EditDish(AdminDishViewModel model)
        {
            try
            {
                if (model.DishEditedCategoryName != "Dryck")
                {
                    model.Dish.Products = _dishRepo.GetProductsByName(model.DishEditedIngredients.ToList());
                }
                model.Dish.DishType = _dishRepo.GetDishTypeByName(model.DishEditedCategoryName).DishType1;             

            }
            catch (Exception ex)
            {
                var adminmodel = _adminService.GetAdminViewModel();

                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

                ViewBag.Errors = "Kategori Krävs";


                return View("Dashboard", adminmodel);
            }


            if (ModelState.IsValid)
            {
                _dishRepo.UpdateDish(model.Dish);
                return RedirectToAction("Dashboard");
            }
            else
            {
                var adminmodel = _adminService.GetAdminViewModel();

                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);

                ViewBag.Errors = String.Join(", ", allErrors.Select(x => x.ErrorMessage));
                

                return View("Dashboard", adminmodel);
            }
            
        }

        [HttpPost]
        public IActionResult UpdateOrder(AdminOrderViewModel model)
        {

            // skriver ut det för att göra det tydligare för läsare
            bool isDelivered = bool.Parse(model.UpdatedOrderStatus);
            _orderRepo.UpdateOrder(isDelivered, model.Order.OrderId);

            return RedirectToAction("Dashboard");

        }



        [HttpPost]
        public async Task<IActionResult> UpdateUser(AdminUserViewModel model) 
        {
            var user = await _userManager.FindByIdAsync(model.User.Id);
            if(await _userManager.IsInRoleAsync(user, model.UserUpdatedRole) == false)
            {
                await _userManager.AddToRoleAsync(user, model.UserUpdatedRole);
            }
            return RedirectToAction("Dashboard");
        }
    }
}
