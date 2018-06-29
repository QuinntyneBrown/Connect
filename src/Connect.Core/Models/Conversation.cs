using Connect.Core.Common;
using System.Collections.Generic;

namespace Connect.Core.Models
{
    public class Conversation: Entity
    {
        public int ConversationId { get; set; }
        public ICollection<Profile> Profiles { get; set; } = new HashSet<Profile>();
        public ICollection<Message> Messages { get; set; } = new HashSet<Message>();
    }
}
