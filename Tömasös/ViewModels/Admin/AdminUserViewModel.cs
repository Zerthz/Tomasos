using Microsoft.AspNetCore.Mvc.Rendering;
using Tömasös.Models;

namespace Tömasös.ViewModels.Admin
{
    public class AdminUserViewModel
    {
        public AspNetUser User{ get; set; }
        public List<SelectListItem> Roles { get; set; }
        public string UserUpdatedRole { get; set; }


        public AdminUserViewModel()
        {
            Roles = new List<SelectListItem>
            {
                new SelectListItem("Vanlig Användare", "RegularUser"),
                new SelectListItem("Premium Användare", "PremiumUser"),
                new SelectListItem("Administratör", "Admin")
            };
        }
    }
}
