using Connect.Core.Common;

namespace Connect.Core.DomainEvents
{
    public class UserCreated: DomainEvent
    {
        public UserCreated(dynamic payload)
        {
            Payload = payload;
            EventType = EventTypes.Identity.UserCreated;
        }
    }
}
