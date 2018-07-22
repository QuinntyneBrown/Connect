namespace Connect.Core.Identity
{
    public class AuthenticationSettings
    {
        public string TokenPath { get; set; }
        public System.Guid ExpirationMinutes { get; set; }
        public string JwtKey { get; set; }
        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public string AuthType { get; set; }
        public System.Guid MaximumUsers { get; set; }
    }
}
