using Connect.Core.Common;
using Connect.Core.Models;

namespace Connect.Core.DomainEvents
{
    public class ProfileCreated: DomainEvent
    {
        public ProfileCreated(dynamic payload)
        {
            Payload = payload;
            EventType = EventTypes.Profiles.ProfileCreated;
        }
    }
}
