using Connect.Core.Common;
using System;
using System.Collections.Generic;

namespace Connect.Core.Models
{
    public class Order: Entity
    {
        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderLineItems { get; set; }
        = new HashSet<OrderItem>();
    }
}
