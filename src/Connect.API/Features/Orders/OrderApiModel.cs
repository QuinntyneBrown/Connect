using Connect.Core.Models;
using System.Collections.Generic;

namespace Connect.API.Features.Orders
{
    public class OrderApiModel
    {        
        public int OrderId { get; set; }
        public string Name { get; set; }
        public int CustomerId { get; set; }
        public ICollection<OrderItemApiModel> Items { get; set; }
        = new HashSet<OrderItemApiModel>();

        public static OrderApiModel FromOrder(Order order)
            => new OrderApiModel
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId
            };
    }
}
