using Connect.API.Features.ContactRequests;
using Connect.Core.Models;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class ContactRequestScenarios: ContactRequestScenarioBase
    {

        [Fact]
        public async Task ShouldSave()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<CreateContactRequestCommand.Request, CreateContactRequestCommand.Response>(Post.ContactRequests, new CreateContactRequestCommand.Request() {
                        ContactRequest = new ContactRequestDto()
                        {
                            Name = "Name",
                        }
                    });
     
	            var entity = context.ContactRequests.First();

                Assert.Equal("Name", entity.Name);
            }
        }

        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetContactRequestsQuery.Response>(Get.ContactRequests);

                Assert.True(response.ContactRequests.Count() > 0);
            }
        }


        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetContactRequestByIdQuery.Response>(Get.ContactRequestById(1));

                Assert.True(response.ContactRequest.ContactRequestId != default(System.Guid));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetContactRequestByIdQuery.Response>(Get.ContactRequestById(1));

                Assert.True(getByIdResponse.ContactRequest.ContactRequestId != default(System.Guid));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<CreateContactRequestCommand.Request, CreateContactRequestCommand.Response>(Post.ContactRequests, new CreateContactRequestCommand.Request()
                    {
                        ContactRequest = getByIdResponse.ContactRequest
                    });

                Assert.True(saveResponse.ContactRequestId != default(System.Guid));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.ContactRequest(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
