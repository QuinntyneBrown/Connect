namespace IntegrationTests.Features
{
    public class ProfileScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string Profiles = "api/profiles";

            public static string ProfileById(int id)
            {
                return $"api/profiles/{id}";
            }
        }

        public static class Post
        {
            public static string Profiles = "api/profiles";
        }

        public static class Delete
        {
            public static string Profile(int id)
            {
                return $"api/profiles/{id}";
            }
        }
    }
}
