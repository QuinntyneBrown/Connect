using Connect.API.Features.Profiles;
using Connect.Core.Models;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class ProfileScenarios: ProfileScenarioBase
    {

        [Fact]
        public async Task ShouldSave()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<SaveProfileCommand.Request, SaveProfileCommand.Response>(Post.Profiles, new SaveProfileCommand.Request() {
                        Profile = new ProfileApiModel()
                        {
                            Name = "Name",
                        }
                    });
     
	            var entity = context.Profiles.First();

                Assert.Equal("Name", entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetProfilesQuery.Response>(Get.Profiles);

                Assert.True(response.Profiles.Count() > 0);
            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetProfileByIdQuery.Response>(Get.ProfileById(1));

                Assert.True(response.Profile.ProfileId != default(int));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetProfileByIdQuery.Response>(Get.ProfileById(1));

                Assert.True(getByIdResponse.Profile.ProfileId != default(int));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<SaveProfileCommand.Request, SaveProfileCommand.Response>(Post.Profiles, new SaveProfileCommand.Request()
                    {
                        Profile = getByIdResponse.Profile
                    });

                Assert.True(saveResponse.ProfileId != default(int));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.Profile(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
