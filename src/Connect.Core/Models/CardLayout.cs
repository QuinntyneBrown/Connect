using Connect.Core.Common;

namespace Connect.Core.Models
{
    public class CardLayout: Entity
    {
        public int CardLayoutId { get; set; }           
		public string Name { get; set; }
        public string Description { get; set; }
    }
}
