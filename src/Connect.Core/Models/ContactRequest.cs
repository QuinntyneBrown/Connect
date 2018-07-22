using Connect.Core.Common;

namespace Connect.Core.Models
{
    public class ContactRequest: AggregateRoot
    {
        public System.Guid ContactRequestId { get; set; }           
		public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
