using Connect.Core.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Connect.Core.Models
{
    public class Profile: AggregateRoot
    {
        public int ProfileId { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        public int ProfileTypeId { get; set; }
        public string Name { get; set; }        
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public ProfileType ProfileType { get; set; }
        public ICollection<ProfileImage> ProfileImages { get; set; }
        = new HashSet<ProfileImage>();
        public User User { get; set; }
    }
}
