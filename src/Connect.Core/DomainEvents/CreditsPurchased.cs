using Connect.Core.Common;
using Connect.Core.Models;

namespace Connect.Core.DomainEvents
{
    public class CreditsPurchased: DomainEvent
    {
        public CreditsPurchased(dynamic payload)
        {
            Payload = payload;
            EventType = EventTypes.Profiles.CreditsPurchased;
        }
    }
}
