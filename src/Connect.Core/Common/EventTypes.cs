namespace Connect.Core.Common
{
    public static class EventTypes
    {
        public static class CardLayouts
        {

        }

        public static class Cards
        {

        }

        public static class Conversations
        {

        }

        public static class DashboardCards
        {

        }

        public static class Dashboards
        {

        }

        public static class DigitalAssets
        {

        }
        
        public static class Identity {
            public const string UserSignedIn = nameof(UserSignedIn);
            public const string UserSignedOut = nameof(UserSignedOut);
        }

        public static class Orders
        {
            public const string OrderCreated = nameof(OrderCreated);
            public const string OrderCancelled = nameof(OrderCancelled);
        }

        public static class Products
        {
            public const string ProductPurchased = nameof(ProductPurchased);
        }

        public static class Profiles
        {
            public const string CreditsPurchased = nameof(CreditsPurchased);
            public const string CreditsConsumed = nameof(CreditsConsumed);
        }

        public static class Reports
        {

        }
    }
}
