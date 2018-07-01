using MediatR;
using System;

namespace Connect.Core.Common
{
    public class DomainEvent: INotification
    {
        public int DomainEventId { get; set; }
        public string EventType { get; set; }
        public string Payload { get; set; }
        public string Subject { get; set; }
        public DateTime EventTime { get; set; }
        public string DataVersion { get; set; }
    }
}
