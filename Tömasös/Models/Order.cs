using System;
using System.Collections.Generic;

namespace Tömasös.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderDishes = new HashSet<OrderDish>();
        }

        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalPrice { get; set; }
        public bool IsDelivered { get; set; }
        public string CustomerId { get; set; } = null!;

        public virtual AspNetUser Customer { get; set; } = null!;
        public virtual ICollection<OrderDish> OrderDishes { get; set; }
    }
}
