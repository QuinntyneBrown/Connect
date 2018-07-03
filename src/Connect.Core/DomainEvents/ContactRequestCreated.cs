using Connect.Core.Common;

namespace Connect.Core.DomainEvents
{
    public class ContactRequestCreated: DomainEvent
    {
        public ContactRequestCreated(dynamic payload)
        {
            Payload = payload;
            EventType = EventTypes.ContactRequests.ContactRequestCreated;
        }
    }
}
