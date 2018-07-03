using Connect.API.Features.Profiles;
using Connect.Core.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class ProfileTypeScenarios : ProfileTypeScenarioBase
    {
        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetProfileTypesQuery.Response>(Get.ProfileTypes);

                Assert.True(response.ProfileTypes.Count() > 0);
            }
        }
    }
}
