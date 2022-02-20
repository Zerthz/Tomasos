using Tömasös.Models;
using Tömasös.ModelsIdentity;

namespace Tömasös.ViewModels
{
    public class ViewProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public List<Order> Orders { get; set; }
    }
}
