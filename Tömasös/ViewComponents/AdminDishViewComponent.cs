using Microsoft.AspNetCore.Mvc;
using Tömasös.ViewModels.Admin;

namespace Tömasös.ViewComponents
{
    public class AdminDishViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(AdminDishViewModel model)
        {

            return View("/Views/Shared/Components/Admin/Dish.cshtml", model); 
        }
    }
}
