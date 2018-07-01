using Connect.Core.Models;

namespace Connect.API.Features.Customers
{
    public class CustomerApiModel
    {        
        public int CustomerId { get; set; }
        public string Name { get; set; }

        public static CustomerApiModel FromCustomer(Customer customer)
        {
            var model = new CustomerApiModel();
            model.CustomerId = customer.CustomerId;
            model.Name = customer.Name;
            return model;
        }
    }
}
