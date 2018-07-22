namespace Connect.Core.Models
{
    public class AccessToken
    {
        public System.Guid AccessTokenId { get; set; }           
		public string Value { get; set; }
        public string Username { get; set; }
        public bool IsValid { get; set; }
    }
}
