using Connect.Core.Common;
using Connect.Core.Models;

namespace Connect.Core.DomainEvents
{
    public class ConversationAccepted: DomainEvent
    {
        public ConversationAccepted(dynamic payload)
        {
            Payload = payload;
            EventType = EventTypes.Conversations.ConversationAccepted;
        }
    }
}
