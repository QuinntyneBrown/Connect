namespace Connect.Core.Models
{
    public class Message
    {
        public int MessageId { get; set; }
        public string Body { get; set; }
        public int FromId { get; set; }
        public int ToId { get; set; }
        public Profile From { get; set; }
        public Profile To { get; set; }
        public int ConversationId { get; set; }
        public Conversation Conversation { get; set; }
    }
}
