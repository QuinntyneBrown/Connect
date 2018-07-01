namespace IntegrationTests.Features
{
    public class DigitalAssetScenarioBase: ScenarioBase
    {
        public static class Get
        {
            public static string DigitalAssets = "api/digitalAssets";

            public static string DigitalAssetById(int id)
            {
                return $"api/digitalAssets/{id}";
            }
        }

        public static class Post
        {
            public static string DigitalAssets = "api/digitalAssets";
        }

        public static class Delete
        {
            public static string DigitalAsset(int id)
            {
                return $"api/digitalAssets/{id}";
            }
        }
    }
}
