using Microsoft.AspNetCore.Mvc.Rendering;
using Tömasös.Models;
using Tömasös.Repository;
using Tömasös.ViewModels.Admin;

namespace Tömasös.Services
{
    public class AdminService : IAdminService
    {
        private readonly ICustomerRepo _customerRepo;
        private readonly IOrderRepo _orderRepo;
        private readonly IDishRepo _dishRepo;
        private readonly IDishService _dishService;

        public AdminService(ICustomerRepo customerRepo, IOrderRepo orderRepo,
            IDishRepo dishRepo, IDishService dishService)
        {
            _customerRepo = customerRepo;
            _orderRepo = orderRepo;
            _dishRepo = dishRepo;
            _dishService = dishService;
        }
        public AdminUserViewModel GetUserViewModel(string id)
        {
            AdminUserViewModel vm = new AdminUserViewModel();
            vm.User = _customerRepo.GetUserById(id);

            return vm;
        }

        public AdminOrderViewModel GetOrderViewModel(int id)
        {
            AdminOrderViewModel vm = new AdminOrderViewModel();
            vm.Order = _orderRepo.GetOrderById(id);
            vm.Order.Customer = _customerRepo.GetUserById(vm.Order.CustomerId);

            return vm;
        }

        public AdminDishViewModel GetDishViewModel(int id)
        {
            AdminDishViewModel vm = new AdminDishViewModel();
            vm.Dish = _dishRepo.GetDishById(id);

            var allProducts = _dishRepo.GetProducts();
            allProducts = allProducts.Except(vm.Dish.Products).ToList();

            foreach (var product in allProducts)
            {
                vm.AllIngredients.Add(new SelectListItem(product.ProductName, product.ProductName));
            }
            foreach (var product in vm.Dish.Products)
            {
                vm.DishCurrentIngredients.Add(new SelectListItem(product.ProductName, product.ProductName));
            }

            return vm;
        }

        public AdminDishViewModel GetEmptyDishViewModel()
        {
            AdminDishViewModel vm = new AdminDishViewModel();
            vm.Dish = new Dish();

            var allProducts = _dishRepo.GetProducts();
            foreach (var product in allProducts)
            {
                vm.AllIngredients.Add(new SelectListItem(product.ProductName, product.ProductName));
            }

            return vm;
        }

        public AdminViewModel GetAdminViewModel()
        {
            AdminViewModel vm = new AdminViewModel();
            vm.DishVM = _dishService.GetModel();
            vm.Orders = _orderRepo.GetActiveOrders();
            vm.Users = _customerRepo.GetUsers();

            return vm;
        }


    }
}
