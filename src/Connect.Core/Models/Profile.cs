using Connect.Core.Common;
using System.Collections.Generic;

namespace Connect.Core.Models
{
    public class Profile: Entity
    {
        public int ProfileId { get; set; }
        public int UserId { get; set; }
        public int ProfileTypeId { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ProfileType ProfileType { get; set; }
        public ICollection<ProfileImage> ProfileImages { get; set; }
        = new HashSet<ProfileImage>();
        public User User { get; set; }
    }
}
