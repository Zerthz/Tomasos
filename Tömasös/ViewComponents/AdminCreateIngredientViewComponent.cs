using Microsoft.AspNetCore.Mvc;

namespace Tömasös.ViewComponents
{
    public class AdminCreateIngredientViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View("/Views/Shared/Components/Admin/CreateIngredient.cshtml");
        }
    }
}
