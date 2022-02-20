using Tömasös.Repository;
using Tömasös.ViewModels;

namespace Tömasös.Services
{
    public class DishService : IDishService
    {
        private readonly IDishRepo _dishrepo;

        public DishService(IDishRepo dishrepo)
        {
            _dishrepo = dishrepo;
        }

        public DishViewModel GetModel()
        {
            var model = new DishViewModel();
            model.Dishes = _dishrepo.GetAllDishes().OrderBy(x=> x.Price).ToList();
            model.Categories = _dishrepo.GetDishTypes();
            return model;
        }
    }
}
