using MediatR;
using System;

namespace Connect.Core.DomainEvents
{
    public class DomainEvent : INotification
    {
        public System.Guid CorrelationId { get; set; }
        public System.Guid CausationId { get; set; }
        public System.Guid ActivityId { get; set; }
    }
}
