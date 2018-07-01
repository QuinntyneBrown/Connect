using Connect.Core.Common;

namespace Connect.Core.DomainEvents
{
    public class OrderCancelled : DomainEvent
    {
        public OrderCancelled (dynamic payload)
        {
            Payload = payload;
            EventType = EventTypes.Orders.OrderCancelled;
        }
    }
}
