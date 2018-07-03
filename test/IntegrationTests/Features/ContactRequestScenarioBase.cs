namespace IntegrationTests.Features
{
    public class ContactRequestScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string ContactRequests = "api/contactRequests";

            public static string ContactRequestById(int id)
            {
                return $"api/contactRequests/{id}";
            }
        }

        public static class Post
        {
            public static string ContactRequests = "api/contactrequests";
        }

        public static class Delete
        {
            public static string ContactRequest(int id)
            {
                return $"api/contactRequests/{id}";
            }
        }
    }
}
