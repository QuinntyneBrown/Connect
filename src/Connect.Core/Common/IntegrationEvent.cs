using MediatR;
using Newtonsoft.Json;
namespace Connect.Core.Common
{
    public class IntegrationEvent: DomainEvent, INotification
    {
        public IntegrationEvent(DomainEvent @event)
        {
            DomainEventId = @event.DomainEventId;
            EventType = @event.EventType;
            Subject = @event.Subject;
            EventTime = @event.EventTime;
            DataVersion = @event.DataVersion;
        }        
    }
}
