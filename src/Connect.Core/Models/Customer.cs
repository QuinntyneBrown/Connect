using Connect.Core.Common;
using System.Collections.Generic;

namespace Connect.Core.Models
{
    public class Customer: Profile
    {
        public Customer()
        {

        }
        public System.Guid CustomerId { get; set; }
        public System.Guid Credits { get; set; }
        public ICollection<Order> Orders { get; set; }
        = new HashSet<Order>();
    }
}
