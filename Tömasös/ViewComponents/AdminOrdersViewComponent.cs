using Microsoft.AspNetCore.Mvc;
using Tömasös.ViewModels.Admin;

namespace Tömasös.ViewComponents
{
    public class AdminOrdersViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(AdminOrderViewModel model)
        {
            return View("/Views/Shared/Components/Admin/Orders.cshtml", model);
        }
    }
}
