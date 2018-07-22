using Connect.Core.Models;

namespace Connect.API.Features.Conversations
{
    public class ConversationDto
    {        
        public System.Guid ConversationId { get; set; }
        public string Name { get; set; }

        public static ConversationDto FromConversation(Conversation conversation)
        {
            var model = new ConversationDto();
            model.ConversationId = conversation.ConversationId;
            
            return model;
        }
    }
}
