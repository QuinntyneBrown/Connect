using MediatR;
using System;
using System.Collections.Generic;

namespace Connect.Core.Common
{
    public class Entity
    {
        public Entity() => _domainEvents = new List<DomainEvent>();
        public DateTime CreatedOn { get; set; }
        public DateTime LastModifiedOn { get; set; }
        public bool IsDeleted { get; set; }
        private List<DomainEvent> _domainEvents;
        public IReadOnlyCollection<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();
        public void RaiseDomainEvent(DomainEvent eventItem) => _domainEvents.Add(eventItem);
        public void ClearEvents() => _domainEvents.Clear();
        public int Version { get; set; }
    }
}
