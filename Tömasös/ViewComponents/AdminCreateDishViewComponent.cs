using Microsoft.AspNetCore.Mvc;
using Tömasös.ViewModels.Admin;

namespace Tömasös.ViewComponents
{
    public class AdminCreateDishViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(AdminDishViewModel model)
        {
            return View("/Views/Shared/Components/Admin/CreateDish.cshtml", model);
        }
    }
}
