using Tömasös.ViewModels.Admin;

namespace Tömasös.Services
{
    public interface IAdminService
    {
        AdminDishViewModel GetDishViewModel(int id);
        AdminOrderViewModel GetOrderViewModel(int id);
        AdminUserViewModel GetUserViewModel(string id);
        AdminDishViewModel GetEmptyDishViewModel();
        AdminViewModel GetAdminViewModel();
    }
}