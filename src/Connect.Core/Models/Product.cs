using Connect.Core.Common;

namespace Connect.Core.Models
{
    public class Product: AggregateRoot
    {
        public int ProductId { get; set; }           
		public string Name { get; set; }
        public string Description { get; set; }
    }
}
