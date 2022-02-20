using Tömasös.Models;

namespace Tömasös.Repository
{
    public interface IDishRepo
    {
        List<Dish> GetAllDishes();
        List<DishType> GetDishTypes();
        Dish GetDishById(int id);
        List<Product> GetProducts();
        List<Product> GetProductsByName(List<string> names);
        void UpdateDish(Dish dish);
        DishType GetDishTypeByName(string name);
        void CreateDish(Dish dish);
        void CreateProduct(Product product);
    }
}