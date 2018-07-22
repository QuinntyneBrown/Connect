using Connect.Core.Models;

namespace Connect.API.Features.Orders
{
    public class OrderItemApiModel
    {
        public int ProductId { get; set; }

        public static OrderItemApiModel FromModel(OrderItem item)
            => new OrderItemApiModel()
            {
                ProductId = item.ProductId
            };
    }
}
