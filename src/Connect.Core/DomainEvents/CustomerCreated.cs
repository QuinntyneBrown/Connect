using Connect.Core.Common;
using Connect.Core.Models;

namespace Connect.Core.DomainEvents
{
    public class CustomerCreated: DomainEvent
    {
        public CustomerCreated(dynamic payload)
        {
            Payload = payload;
            EventType = EventTypes.Customers.CustomerCreated;
        }
    }
}
