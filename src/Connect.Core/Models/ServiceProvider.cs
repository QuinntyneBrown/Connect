using Connect.Core.Common;

namespace Connect.Core.Models
{
    public class ServiceProvider: Profile
    {
        public ServiceProvider()
        {
            ProfileTypeId = (int)ProfileTypes.ServiceProvider;
        }
        public int ServiceProviderId { get; set; }           		        
    }
}
