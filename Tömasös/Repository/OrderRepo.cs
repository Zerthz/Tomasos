using Microsoft.EntityFrameworkCore;
using Tömasös.Models;

namespace Tömasös.Repository
{
    public class OrderRepo : IOrderRepo
    {
        private readonly TomasosDbContext _context;

        public OrderRepo(TomasosDbContext context)
        {
            _context = context;
        }

        public int InsertOrder(Order order)
        {
            _context.Orders.Add(order);
            _context.SaveChanges();

            return order.OrderId;
            
        }

        public void InsertOrderDishes(List<OrderDish> orderDishes)
        {
            _context.OrderDishes.AddRange(orderDishes);
            _context.SaveChanges();
        }
        public Order GetOrderById(int id)
        {
            return _context.Orders
                .Include(o => o.OrderDishes)
                .ThenInclude(d=> d.Dish)
                .FirstOrDefault(x => x.OrderId == id) ?? throw new Exception("Ordern finns inte i databasen");
        }
        public void UpdateOrder(bool isDelivered, int orderId)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == orderId);
            order.IsDelivered = isDelivered;
            _context.SaveChanges();
        }

        public void RemoveOrder(int id)
        {
            var entry = _context.Orders.Include(o=> o.OrderDishes).FirstOrDefault(x => x.OrderId == id);
            _context.Orders.Remove(entry);
            _context.SaveChanges();
        }

        public List<Order> GetActiveOrders()
        {
            return _context.Orders
                .Where(x=> x.IsDelivered == false)
                .Include(x=> x.OrderDishes)
                .ThenInclude(d => d.Dish)
                .ToList();
                
        }

        public List<Order> GetOrdersFromUser(string userId)
        {
            return _context.Orders
                  .Where(x=>x.CustomerId == userId)
                  .Include(x => x.OrderDishes)
                     .ThenInclude(x => x.Dish)
                 .ToList();
        }


    }
}
