namespace Connect.Core.Models
{
    public class Message
    {
        public System.Guid MessageId { get; set; }
        public string Body { get; set; }
        public System.Guid FromId { get; set; }
        public System.Guid ToId { get; set; }
        public Profile From { get; set; }
        public Profile To { get; set; }
        public System.Guid ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
