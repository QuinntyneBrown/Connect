namespace Connect.Core.Models
{
    public class ProfileImage
    {
        public System.Guid ProfileImageId { get; set; }
        public System.Guid ProfileId { get; set; }
        public string Url { get; set; }
        public Profile Profile { get; set; }
    }
}
