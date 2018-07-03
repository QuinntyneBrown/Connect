using Connect.API.Features.Profiles;
using Connect.Core.Models;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Connect.Core.Common;

namespace IntegrationTests.Features
{
    public class ProfileScenarios: ProfileScenarioBase
    {

        [Fact]
        public async Task ShouldCreate()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<CreateProfileCommand.Request, CreateProfileCommand.Response>(Post.Profiles, new CreateProfileCommand.Request() {
                        Name = "Name",
                        ProfileTypeId = (int)ProfileTypes.Customer,
                        Username = "profileUsername",
                        Password = "P@ssw0rd",
                        ConfirmPassword = "P@ssw0rd"
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

                Assert.True(response.Profile.ProfileTypeId != default(int));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetProfileByIdQuery.Response>(Get.ProfileById(1));

                Assert.True(getByIdResponse.Profile.ProfileTypeId != default(int));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<CreateProfileCommand.Request, CreateProfileCommand.Response>(Post.Profiles, new CreateProfileCommand.Request()
                    {
                        ProfileTypeId = getByIdResponse.Profile.ProfileTypeId
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
