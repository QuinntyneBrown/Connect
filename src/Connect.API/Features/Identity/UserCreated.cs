using Connect.Core.Common;
using Newtonsoft.Json;

namespace Connect.API.Features.Identity
{
    public class UserCreated: IntegrationEvent
    {
        public UserCreated(Core.DomainEvents.UserCreated @event)
            :base(@event) { }

        public static UserCreated FromDomainEvent(Core.DomainEvents.UserCreated @event) {
            return new UserCreated(@event) { 
                Payload = JsonConvert.SerializeObject(@event.Payload)			
            };
        }
    }
}
