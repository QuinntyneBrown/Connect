using Connect.Core.Models;

namespace Connect.API.Features.Customers
{
    public class CustomerDto
    {        
        public int CustomerId { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public int ProfileId { get; set; }
        public static CustomerDto FromCustomer(Customer customer)
            => new CustomerDto
            {
                CustomerId = customer.CustomerId,
                Name = customer.Name,
                Credits = customer.Credits,
                ProfileId = customer.ProfileId
            };
    }
}
