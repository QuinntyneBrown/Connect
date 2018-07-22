using Connect.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Connect.Core.Models
{
    public class Order: AggregateRoot
    {
        public Order()
        {

        }

        public void AddOrderItem(System.Guid productId, string productName, decimal unitPrice, decimal discount, string pictureUrl)
        {            
            var orderItem = new OrderItem() {
                ProductId = productId
            };

            OrderItems.Add(orderItem);
        }

        public void SetAwaitingPayment()
        {            

        }

        public void SetPaidStatus()
        {

        }


        public System.Guid OrderId { get; set; }
        public System.Guid OrderStatusId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public System.Guid CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        = new HashSet<OrderItem>();
    }
}
