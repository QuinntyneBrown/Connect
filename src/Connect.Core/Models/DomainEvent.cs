namespace Connect.Core.Models
{
    public class DomainEvent
    {
        public int DomainEventId { get; set; }
        public string Type { get; set; }
        public string Payload { get; set; }
    }
}
