using Microsoft.AspNetCore.Mvc;
using Tömasös.Models;
using Tömasös.ModelsIdentity;
using Tömasös.ViewModels.Admin;

namespace Tömasös.ViewComponents
{
    public class AdminCustomersViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(AdminUserViewModel model)
        {
            return View("/Views/Shared/Components/Admin/Customer.cshtml", model);
        }
    }
}
