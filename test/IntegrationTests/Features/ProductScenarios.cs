using Connect.API.Features.Products;
using Connect.Core.Extensions;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace IntegrationTests.Features
{
    public class ProductScenarios: ProductScenarioBase
    {        
        [Fact]
        public async Task ShouldGetAll()
        {
            using (var server = CreateServer())
            {
                var response = await server.CreateClient()
                    .GetAsync<GetProductsQuery.Response>(Get.Products);

                Assert.True(response.Products.Count() > 0);
            }
        }        
    }
}
