using Connect.Core.Common;

namespace Connect.Core.Models
{
    public class ContactRequest: AggregateRoot
    {
        public int ContactRequestId { get; set; }           
		public string Name { get; set; }
        public string Email { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
    }
}
