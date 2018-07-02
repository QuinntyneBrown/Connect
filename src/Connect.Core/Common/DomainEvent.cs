using MediatR;
using System;

namespace Connect.Core.Common
{
    public class DomainEvent: INotification
    {
        public Guid DomainEventId { get; set; }
        public string EventType { get; set; }
        public dynamic Payload { get; set; }
        public string Subject { get; set; }
        public DateTime EventTime { get; set; }
        public string DataVersion { get; set; }

    }
}
