using Connect.Core.Common;
using Newtonsoft.Json;

namespace Connect.API.Features.Conversations
{
    public class MessageRead: IntegrationEvent
    {
        public MessageRead(Core.DomainEvents.MessageRead @event)
            :base(@event) { }

        public static MessageRead FromDomainEvent(Core.DomainEvents.MessageRead @event) {
            return new MessageRead(@event) { 
                Payload = JsonConvert.SerializeObject(@event.Payload)			
            };
        }
    }
}
