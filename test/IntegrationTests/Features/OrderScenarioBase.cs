namespace IntegrationTests.Features
{
    public class OrderScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string Orders = "api/orders";

            public static string OrderById(int id)
            {
                return $"api/orders/{id}";
            }
        }

        public static class Post
        {
            public static string Orders = "api/orders";
        }

        public static class Delete
        {
            public static string Order(int id)
            {
                return $"api/orders/{id}";
            }
        }
    }
}
