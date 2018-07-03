using Connect.Core.Common;
using Newtonsoft.Json;

namespace Connect.API.Features.ContactRequests
{
    public class ContactRequestCreated: IntegrationEvent
    {
        public ContactRequestCreated(Core.DomainEvents.ContactRequestCreated @event)
            :base(@event) { }

        public static ContactRequestCreated FromDomainEvent(Core.DomainEvents.ContactRequestCreated @event) {
            return new ContactRequestCreated(@event) { 
                Payload = JsonConvert.SerializeObject(@event.Payload)			
            };
        }
    }
}
