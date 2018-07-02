using Connect.Core.Common;
using Newtonsoft.Json;

namespace Connect.API.IntegrationEvents
{
    public class ConversationAccepted: IntegrationEvent
    {
        public ConversationAccepted(Core.DomainEvents.ConversationAccepted @event)
            :base(@event) { }

        public static ConversationAccepted FromDomainEvent(Core.DomainEvents.ConversationAccepted @event) {
            return new ConversationAccepted(@event) {
                Payload = JsonConvert.SerializeObject(@event.Payload)
            };
        }
    }
}
