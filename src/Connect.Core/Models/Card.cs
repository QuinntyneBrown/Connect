using Connect.Core.Common;

namespace Connect.Core.Models
{
    public class Card: Entity
    {
        public int CardId { get; set; }           
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
