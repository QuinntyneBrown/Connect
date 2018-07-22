using Connect.Core.Common;
using Connect.Core.DomainEvents;
using System;

namespace Connect.Core.Models
{
    public class Role: AggregateRoot
    {
        public Role(string name)
            => Apply(new RoleCreated(name));
        public System.Guid RoleId { get; set; }           
		public string Name { get; set; }
        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch(@event)
            {
                case RoleCreated roleCreated:
                    Name = roleCreated.Name;
                    break;
            }
        }
    }
}
