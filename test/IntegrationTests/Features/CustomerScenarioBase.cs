namespace IntegrationTests.Features
{
    public class CustomerScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string Customers = "api/customers";

            public static string CustomerById(int id)
            {
                return $"api/customers/{id}";
            }
        }

        public static class Post
        {
            public static string Customers = "api/customers";
        }

        public static class Delete
        {
            public static string Customer(int id)
            {
                return $"api/customers/{id}";
            }
        }
    }
}
