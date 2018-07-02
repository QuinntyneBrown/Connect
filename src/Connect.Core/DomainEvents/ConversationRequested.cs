using Connect.Core.Common;
using Connect.Core.Models;

namespace Connect.Core.DomainEvents
{
    public class ConversationRequested: DomainEvent
    {
        public ConversationRequested(dynamic payload)
        {
            Payload = payload;
            EventType = EventTypes.Conversations.ConversationRequested;
        }
    }
}
