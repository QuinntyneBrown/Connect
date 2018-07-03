using Connect.Core.Common;
using Connect.Core.Models;

namespace Connect.Core.DomainEvents
{
    public class OrderCreated: DomainEvent
    {
        public OrderCreated(dynamic payload)
        {
            EventData = new { Order = payload };
            EventType = EventTypes.Orders.OrderCreated;
        }
    }
}
