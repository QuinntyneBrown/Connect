using Connect.API.Features.Dashboards;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class DashboardScenarios: DashboardScenarioBase
    {

        [Fact]
        public async Task ShouldSave()
        {
            using (var server = CreateServer())
            {
                IAppDbContext context = server.Host.Services.GetService(typeof(IAppDbContext)) as IAppDbContext;

                var response = await server.CreateClient()
                    .PostAsAsync<SaveDashboardCommand.Request, SaveDashboardCommand.Response>(Post.Dashboards, new SaveDashboardCommand.Request() {
                        Dashboard = new DashboardDto()
                        {
                            Name = "Name",
                        }
                    });
     
	            var entity = context.Dashboards.First();

                Assert.Equal("Name", entity.Name);
            }
        }
        

        [Fact]
        public async Task ShouldGetById()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetDashboardByIdQuery.Response>(Get.DashboardById(1));

                Assert.True(response.Dashboard.DashboardId != default(System.Guid));
            }
        }
        
        [Fact]
        public async Task ShouldUpdate()
        {
            using (var server = CreateServer())
            {
                var getByIdResponse = await server.CreateClient()
                    .GetAsync<GetDashboardByIdQuery.Response>(Get.DashboardById(1));

                Assert.True(getByIdResponse.Dashboard.DashboardId != default(System.Guid));

                var saveResponse = await server.CreateClient()
                    .PostAsAsync<SaveDashboardCommand.Request, SaveDashboardCommand.Response>(Post.Dashboards, new SaveDashboardCommand.Request()
                    {
                        Dashboard = getByIdResponse.Dashboard
                    });

                Assert.True(saveResponse.DashboardId != default(System.Guid));
            }
        }
        
        [Fact]
        public async Task ShouldDelete()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .DeleteAsync(Delete.Dashboard(1));

                response.EnsureSuccessStatusCode();
            }
        }
    }
}
