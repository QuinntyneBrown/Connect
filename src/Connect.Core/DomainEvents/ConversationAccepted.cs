using Connect.Core.Common;

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
