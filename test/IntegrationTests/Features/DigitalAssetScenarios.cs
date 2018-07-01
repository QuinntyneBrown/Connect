using Connect.API.Features.DigitalAssets;
using Connect.Core.Models;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class DigitalAssetScenarios: DigitalAssetScenarioBase
    {

        [Fact]
        public async Task ShouldSave()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<SaveDigitalAssetCommand.Request, SaveDigitalAssetCommand.Response>(Post.DigitalAssets, new SaveDigitalAssetCommand.Request() {
                        DigitalAsset = new DigitalAssetApiModel()
                        {
                            Name = "Name",
                        }
                    });
     
	            var entity = context.DigitalAssets.First();

                Assert.Equal("Name", entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetDigitalAssetsQuery.Response>(Get.DigitalAssets);

                Assert.True(response.DigitalAssets.Count() > 0);
            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetDigitalAssetByIdQuery.Response>(Get.DigitalAssetById(1));

                Assert.True(response.DigitalAsset.DigitalAssetId != default(Guid));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetDigitalAssetByIdQuery.Response>(Get.DigitalAssetById(1));

                Assert.True(getByIdResponse.DigitalAsset.DigitalAssetId != default(Guid));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<SaveDigitalAssetCommand.Request, SaveDigitalAssetCommand.Response>(Post.DigitalAssets, new SaveDigitalAssetCommand.Request()
                    {
                        DigitalAsset = getByIdResponse.DigitalAsset
                    });

                Assert.True(saveResponse.DigitalAssetId != default(Guid));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.DigitalAsset(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
