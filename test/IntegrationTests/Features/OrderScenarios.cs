using Connect.API.Features.Orders;
using Connect.Core.Models;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Connect.API.Features.Profiles;
using Connect.Core.Common;
using Connect.API.Sagas;

namespace IntegrationTests.Features
{
    public class OrderScenarios: OrderScenarioBase
    {

        [Fact]
        public async Task ShouldCreate()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                await server.CreateClient()
                    .PostAsAsync<CreateProfileProcessManager.Request, CreateProfileProcessManager.Response>(ProfileScenarioBase.Post.Profiles, new CreateProfileProcessManager.Request()
                    {
                        Name = "Name",

                        Username = "profileUsername",
                        Password = "P@ssw0rd",
                        ConfirmPassword = "P@ssw0rd"
                    });

                var response = await server.CreateClient()
                    .PostAsAsync<CreateOrderProcessManager.Request, CreateOrderProcessManager.Response>(Post.Orders, new CreateOrderProcessManager.Request() {
                    });
     
	            var entity = context.Orders.First();
                
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetOrdersQuery.Response>(Get.Orders);

                Assert.True(response.Orders.Count() > 0);
            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetOrderByIdQuery.Response>(Get.OrderById(1));

                Assert.True(response.Order.OrderId != default(System.Guid));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetOrderByIdQuery.Response>(Get.OrderById(1));

                Assert.True(getByIdResponse.Order.OrderId != default(System.Guid));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<CreateOrderCommand.Request, CreateOrderCommand.Response>(Post.Orders, new CreateOrderCommand.Request()
                    {
                        Order = getByIdResponse.Order
                    });

                Assert.True(saveResponse.Order.OrderId != default(System.Guid));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.Order(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
