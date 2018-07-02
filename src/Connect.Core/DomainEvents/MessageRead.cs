using Connect.Core.Common;

namespace Connect.Core.DomainEvents
{
    public class MessageRead: DomainEvent
    {
        public MessageRead(dynamic payload)
        {
            Payload = payload;
            EventType = EventTypes.Conversations.MessageRead;
        }
    }
}
