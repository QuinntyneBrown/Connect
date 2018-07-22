using System;

namespace Connect.Core.DomainEvents
{
    public class RoleCreated: DomainEvent
    {
        public RoleCreated(string name)
            => Name = name;
        public string Name { get; set; }
    }
}
