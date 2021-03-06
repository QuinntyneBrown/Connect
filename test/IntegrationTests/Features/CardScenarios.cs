using Connect.API.Features.Cards;
using Connect.Core.Models;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class CardScenarios: CardScenarioBase
    {

        [Fact]
        public async Task ShouldSave()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<SaveCardCommand.Request, SaveCardCommand.Response>(Post.Cards, new SaveCardCommand.Request() {
                        Card = new CardDto()
                        {
                            Name = "Name",
                        }
                    });
     
	            var entity = context.Cards.First();

                Assert.Equal("Name", entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetCardsQuery.Response>(Get.Cards);

                Assert.True(response.Cards.Count() > 0);
            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetCardByIdQuery.Response>(Get.CardById(1));

                Assert.True(response.Card.CardId != default(System.Guid));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetCardByIdQuery.Response>(Get.CardById(1));

                Assert.True(getByIdResponse.Card.CardId != default(System.Guid));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<SaveCardCommand.Request, SaveCardCommand.Response>(Post.Cards, new SaveCardCommand.Request()
                    {
                        Card = getByIdResponse.Card
                    });

                Assert.True(saveResponse.CardId != default(System.Guid));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.Card(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
