using Connect.Core.Models;

namespace Connect.API.Features.Orders
{
    public class OrderItemDto
    {
        public System.Guid ProductId { get; set; }

        public static OrderItemDto FromModel(OrderItem item)
            => new OrderItemDto()
            {
                ProductId = item.ProductId
            };
    }
}
