using Connect.Core.Common;
using System.Collections.Generic;

namespace Connect.Core.Models
{
    public class Dashboard: AggregateRoot
    {
        public int DashboardId { get; set; }           
        public string Name { get; set; }        
        public int ProfileId { get; set; }
        public ICollection<DashboardCard> DashboardCards { get; set; } 
            = new HashSet<DashboardCard>();
    }
}
