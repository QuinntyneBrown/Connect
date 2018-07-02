using Connect.Core.Common;
using System.Collections.Generic;

namespace Connect.Core.Models
{
    public class Customer: Profile
    {
        public Customer()
        {
            ProfileTypeId = (int)ProfileTypes.Customer;
        }
        public int CustomerId { get; set; }
        public int Credits { get; set; }
        public ICollection<Order> Orders { get; set; }
        = new HashSet<Order>();
    }
}
