using Connect.API.Features.Customers;
using Connect.Core.Models;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class CustomerScenarios: CustomerScenarioBase
    {

        [Fact]
        public async Task ShouldSave()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<CreateCustomerCommand.Request, CreateCustomerCommand.Response>(Post.Customers, new CreateCustomerCommand.Request() {
                        Customer = new CustomerApiModel()
                        {
                            Name = "Name",
                        }
                    });
     
	            var entity = context.Customers.First();

                Assert.Equal("Name", entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetCustomersQuery.Response>(Get.Customers);

                Assert.True(response.Customers.Count() > 0);
            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetCustomerByIdQuery.Response>(Get.CustomerById(1));

                Assert.True(response.Customer.CustomerId != default(int));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetCustomerByIdQuery.Response>(Get.CustomerById(1));

                Assert.True(getByIdResponse.Customer.CustomerId != default(int));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<CreateCustomerCommand.Request, CreateCustomerCommand.Response>(Post.Customers, new CreateCustomerCommand.Request()
                    {
                        Customer = getByIdResponse.Customer
                    });

                Assert.True(saveResponse.CustomerId != default(int));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.Customer(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
