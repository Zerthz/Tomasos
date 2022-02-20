using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tömasös.Repository;
using Tömasös.Services;

namespace Tömasös.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDishService _dishService;

        public HomeController(IDishService dishService)
        {
            _dishService = dishService;
        }
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult Menu()
        {
            var model = _dishService.GetModel();
            return View(model);
        }
    }
}
