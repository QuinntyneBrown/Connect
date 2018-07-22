using Connect.Core.Models;
using System.Collections.Generic;

namespace Connect.API.Features.Orders
{
    public class OrderDto
    {        
        public System.Guid OrderId { get; set; }
        public string Name { get; set; }
        public System.Guid CustomerId { get; set; }
        public ICollection<OrderItemDto> Items { get; set; }
        = new HashSet<OrderItemDto>();

        public static OrderDto FromOrder(Order order)
            => new OrderDto
            {
                OrderId = order.OrderId,
                CustomerId = order.CustomerId
            };
    }
}
