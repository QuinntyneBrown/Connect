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
            OrderStatusId = (int)OrderStatuses.Started;
        }

        public void AddOrderItem(int productId, string productName, decimal unitPrice, decimal discount, string pictureUrl)
        {            
            var orderItem = new OrderItem() {
                ProductId = productId
            };

            OrderItems.Add(orderItem);
        }

        public void SetAwaitingPayment()
        {            
            OrderStatusId = (int)OrderStatuses.AwaitingPayment;
        }

        public void SetPaidStatus()
        {
            if (OrderStatusId != (int)OrderStatuses.AwaitingPayment)
                throw new Exception();

            OrderStatusId = (int)OrderStatuses.Paid;
        }


        public int OrderId { get; set; }
        public int OrderStatusId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public int CustomerId { get; set; }
        public OrderStatus OrderStatus { get; set; }
        public Customer Customer { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        = new HashSet<OrderItem>();
    }
}
