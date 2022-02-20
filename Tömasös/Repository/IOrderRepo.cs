using Tömasös.Models;

namespace Tömasös.Repository
{
    public interface IOrderRepo
    {
        int InsertOrder(Order order);
        void InsertOrderDishes(List<OrderDish> orderDishes);
        List<Order> GetOrdersFromUser(string userId);
        List<Order> GetActiveOrders();
        Order GetOrderById(int id);
        void UpdateOrder(bool isDelivered, int orderId);
        void RemoveOrder(int id);
    }
}