using Connect.Core.Common;
using Connect.Core.DomainEvents;

namespace Connect.Core.Models
{
    public class Card: AggregateRoot
    {
        public int CardId { get; set; }           
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
