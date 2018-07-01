namespace Connect.Core.Models
{
    public class ProfileImage
    {
        public int ProfileImageId { get; set; }
        public int ProfileId { get; set; }
        public string Url { get; set; }
        public Profile Profile { get; set; }
    }
}
