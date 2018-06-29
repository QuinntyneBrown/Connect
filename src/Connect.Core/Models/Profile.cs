using Connect.Core.Common;

namespace Connect.Core.Models
{
    public class Profile: Entity
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public int ProfileTypeId { get; set; }
        public string Name { get; set; }
        public ProfileType ProfileType { get; set; }
        public User User { get; set; }

    }
}
