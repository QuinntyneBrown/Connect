using Connect.Core.Common;
using Connect.Core.Models;

namespace Connect.Core.DomainEvents
{
    public class CreditsConsumed: DomainEvent
    {
        public CreditsConsumed(dynamic payload)
        {
            Payload = payload;
            EventType = EventTypes.Profiles.CreditsConsumed;
        }
    }
}
