using Connect.Core.Common;
using Connect.Core.DomainEvents;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Connect.Core.Models
{
    public class User : AggregateRoot
    {
        public User()
        {
            Salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(Salt);
            }
        }

        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        = new HashSet<UserRole>();

        public void SignOut() => Apply(new UserSignedOut());

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch(@event)
            {
                case UserSignedOut userSignedOut:
                    break;
            }
        }
    }
}
