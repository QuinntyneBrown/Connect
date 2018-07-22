using Connect.Core.Common;

namespace Connect.Core.Models
{
    public class CardLayout: AggregateRoot
    {
        public System.Guid CardLayoutId { get; set; }           
		public string Name { get; set; }
        public string Description { get; set; }
    }
}
