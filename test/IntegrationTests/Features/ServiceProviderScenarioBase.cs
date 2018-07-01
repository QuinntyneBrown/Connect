namespace IntegrationTests.Features
{
    public class ServiceProviderScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string ServiceProviders = "api/serviceProviders";

            public static string ServiceProviderById(int id)
            {
                return $"api/serviceProviders/{id}";
            }
        }

        public static class Post
        {
            public static string ServiceProviders = "api/serviceProviders";
        }

        public static class Delete
        {
            public static string ServiceProvider(int id)
            {
                return $"api/serviceProviders/{id}";
            }
        }
    }
}
