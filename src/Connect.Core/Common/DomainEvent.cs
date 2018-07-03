using MediatR;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connect.Core.Common
{
    public class DomainEvent: INotification
    {
        public Guid DomainEventId { get; set; }
        public string EventType { get; set; }
        public string Payload { get; set; }
        public string Subject { get; set; }
        public DateTime EventTime { get; set; }
        public string DataVersion { get; set; }
        [NotMapped]
        public dynamic EventData { get; set; }

    }
}
