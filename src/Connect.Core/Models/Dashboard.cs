using Connect.Core.Common;
using System.Collections.Generic;

namespace Connect.Core.Models
{
    public class Dashboard: AggregateRoot
    {
        public System.Guid DashboardId { get; set; }           
        public string Name { get; set; }        
        public System.Guid ProfileId { get; set; }
        public ICollection<DashboardCard> DashboardCards { get; set; } 
            = new HashSet<DashboardCard>();
    }
}
