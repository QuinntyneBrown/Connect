using Connect.API.Features.Identity;
using Connect.Core.Extensions;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class UserScenarios: UserScenarioBase
    {
        [Fact]
        public async Task ShouldAuthenticateUser()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .PostAsAsync<AuthenticateCommand.Request, AuthenticateCommand.Response>(Post.Token, new AuthenticateCommand.Request()
                    {
                        Username = "quinntyne@hotmail.com",
                        Password = "P@ssw0rd"
                    });

                Assert.True(response.UserId == 2);
                Assert.True(response.AccessToken != default(string));
            }
        }
    }
}
