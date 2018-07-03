namespace IntegrationTests.Features
{
    public class ProfileTypeScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string ProfileTypes = "api/profileTypes";

            public static string ProfileTypeById(int id)
            {
                return $"api/profileTypes/{id}";
            }
        }

        public static class Post
        {
            public static string ProfileTypes = "api/profileTypes";
        }

        public static class Delete
        {
            public static string ProfileType(int id)
            {
                return $"api/profileTypes/{id}";
            }
        }
    }
}
