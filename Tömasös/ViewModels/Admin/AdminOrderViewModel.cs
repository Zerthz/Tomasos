using Microsoft.AspNetCore.Mvc.Rendering;
using Tömasös.Models;

namespace Tömasös.ViewModels.Admin
{
    public class AdminOrderViewModel
    {
        public Order Order{ get; set; }
        public List<SelectListItem> OrderStatuses { get; set; }
        public string UpdatedOrderStatus { get; set; }

        public AdminOrderViewModel()
        {
            OrderStatuses = new List<SelectListItem>
            {
                new SelectListItem("Inte Levererad", "false"),
                new SelectListItem("Levererad", "true")
            };
        }
    }

}
