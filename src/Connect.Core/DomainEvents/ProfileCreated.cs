using Connect.Core.Common;

namespace Connect.Core.DomainEvents
{
    public class ProfileCreated: DomainEvent
    {
        public ProfileCreated(dynamic payload)
        {
            EventData = new { Profile = payload };
            EventType = EventTypes.Profiles.ProfileCreated;
        }
    }
}
