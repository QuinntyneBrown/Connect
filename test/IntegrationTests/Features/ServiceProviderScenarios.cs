using Connect.API.Features.ServiceProviders;
using Connect.Core.Models;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class ServiceProviderScenarios: ServiceProviderScenarioBase
    {

        [Fact]
        public async Task ShouldSave()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<CreateServiceProviderCommand.Request, CreateServiceProviderCommand.Response>(Post.ServiceProviders, new CreateServiceProviderCommand.Request() {
                        ServiceProvider = new ServiceProviderDto()
                        {
                            Name = "Name",
                        }
                    });
     
	            var entity = context.ServiceProviders.First();

                Assert.Equal("Name", entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetServiceProvidersQuery.Response>(Get.ServiceProviders);

                Assert.True(response.ServiceProviders.Count() > 0);
            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetServiceProviderByIdQuery.Response>(Get.ServiceProviderById(1));

                Assert.True(response.ServiceProvider.ServiceProviderId != default(int));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetServiceProviderByIdQuery.Response>(Get.ServiceProviderById(1));

                Assert.True(getByIdResponse.ServiceProvider.ServiceProviderId != default(int));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<CreateServiceProviderCommand.Request, CreateServiceProviderCommand.Response>(Post.ServiceProviders, new CreateServiceProviderCommand.Request()
                    {
                        ServiceProvider = getByIdResponse.ServiceProvider
                    });

                Assert.True(saveResponse.ServiceProviderId != default(int));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.ServiceProvider(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
