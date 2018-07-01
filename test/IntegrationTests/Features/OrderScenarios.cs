using Connect.API.Features.Orders;
using Connect.Core.Models;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class OrderScenarios: OrderScenarioBase
    {

        [Fact]
        public async Task ShouldSave()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<CreateOrderCommand.Request, CreateOrderCommand.Response>(Post.Orders, new CreateOrderCommand.Request() {
                        Order = new OrderApiModel()
                        {

                        }
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

                Assert.True(response.Order.OrderId != default(int));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetOrderByIdQuery.Response>(Get.OrderById(1));

                Assert.True(getByIdResponse.Order.OrderId != default(int));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<CreateOrderCommand.Request, CreateOrderCommand.Response>(Post.Orders, new CreateOrderCommand.Request()
                    {
                        Order = getByIdResponse.Order
                    });

                Assert.True(saveResponse.OrderId != default(int));
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
