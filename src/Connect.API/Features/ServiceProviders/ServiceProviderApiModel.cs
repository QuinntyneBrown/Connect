using Connect.Core.Models;

namespace Connect.API.Features.ServiceProviders
{
    public class ServiceProviderDto
    {        
        public int ServiceProviderId { get; set; }
        public string Name { get; set; }

        public static ServiceProviderDto FromServiceProvider(ServiceProvider serviceProvider)
        {
            var model = new ServiceProviderDto();
            model.ServiceProviderId = serviceProvider.ServiceProviderId;
            model.Name = serviceProvider.Name;
            return model;
        }
    }
}
