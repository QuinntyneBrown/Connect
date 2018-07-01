using Connect.Core.Common;
using Connect.Core.Models;

namespace Connect.Core.DomainEvents
{
    public class ProductPurchased: DomainEvent
    {
        public ProductPurchased(dynamic payload)
        {
            Payload = payload;
            EventType = EventTypes.Products.ProductPurchased;
        }
    }
}
