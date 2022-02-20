using Microsoft.EntityFrameworkCore;
using Tömasös.Models;

namespace Tömasös.Repository
{
    public class DishRepo : IDishRepo
    {
        private readonly TomasosDbContext _context;

        public DishRepo(TomasosDbContext context)
        {
            _context = context;
        }
        public List<Dish> GetAllDishes()
        {
            return _context.Dishes.Include(x => x.Products).ToList();
        }

        public List<DishType> GetDishTypes()
        {
            return _context.DishTypes.Include(x=> x.Dishes).ToList();
        }

        public DishType GetDishTypeByName(string name)
        {
            return _context.DishTypes.FirstOrDefault(x => x.Description == name) ?? throw new Exception("Mat kategorin finns inte i databasen");
        }
        public Dish GetDishById(int id)
        {
            return _context.Dishes
                .Include(x => x.Products)
                .Include(x=> x.DishTypeNavigation)
                .FirstOrDefault(x => x.DishId == id) ?? throw new NullReferenceException("Id " + id + " finns inte i databasen");
        }

        public void CreateProduct(Product product)
        {
            if (product == null)
                return;

            _context.Products.Add(product);
            _context.SaveChanges();
        }
        public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public List<Product> GetProductsByName(List<string> names)
        {
            var products = new List<Product>();
            foreach (var name in names)
            {
                products.Add(_context.Products.FirstOrDefault(x=> x.ProductName == name));
            }
            return products; 
        }


        public void UpdateDish(Dish dish)
        {
            var entry = _context.Dishes.Include(x=> x.Products).FirstOrDefault(x=> x.DishId == dish.DishId);
            entry.Description = dish.Description;
            entry.DishName = dish.DishName;
            entry.Products = dish.Products;
            entry.Price = dish.Price;
            entry.DishType = dish.DishType;
            _context.SaveChanges();
        }

        public void CreateDish(Dish dish)
        {
            _context.Dishes.Add(dish);
            _context.SaveChanges();
        }
    }
}
