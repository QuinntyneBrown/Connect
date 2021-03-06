using Connect.API.Features.CardLayouts;
using Connect.Core.Models;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class CardLayoutScenarios: CardLayoutScenarioBase
    {

        [Fact]
        public async Task ShouldSave()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<SaveCardLayoutCommand.Request, SaveCardLayoutCommand.Response>(Post.CardLayouts, new SaveCardLayoutCommand.Request() {
                        CardLayout = new CardLayoutDto()
                        {
                            Name = "Name",
                        }
                    });
     
	            var entity = context.CardLayouts.First();

                Assert.Equal("Name", entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetCardLayoutsQuery.Response>(Get.CardLayouts);

                Assert.True(response.CardLayouts.Count() > 0);
            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetCardLayoutByIdQuery.Response>(Get.CardLayoutById(1));

                Assert.True(response.CardLayout.CardLayoutId != default(System.Guid));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetCardLayoutByIdQuery.Response>(Get.CardLayoutById(1));

                Assert.True(getByIdResponse.CardLayout.CardLayoutId != default(System.Guid));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<SaveCardLayoutCommand.Request, SaveCardLayoutCommand.Response>(Post.CardLayouts, new SaveCardLayoutCommand.Request()
                    {
                        CardLayout = getByIdResponse.CardLayout
                    });

                Assert.True(saveResponse.CardLayoutId != default(System.Guid));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.CardLayout(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
