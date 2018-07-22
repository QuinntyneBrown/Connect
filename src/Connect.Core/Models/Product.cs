using Connect.Core.Common;

namespace Connect.Core.Models
{
    public class Product: AggregateRoot
    {
        public System.Guid ProductId { get; set; }           
		public string Name { get; set; }
        public string Description { get; set; }
    }
}
