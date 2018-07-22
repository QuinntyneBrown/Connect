using System.ComponentModel.DataAnnotations.Schema;

namespace Connect.Core.Models
{
    public class UserRole
    {
        [ForeignKey("User")]
        public System.Guid UserId { get; set; }
        [ForeignKey("Role")]
        public System.Guid RoleId { get; set; }
        public User User { get; set; }
        public Role Role { get; set; }
    }
}
