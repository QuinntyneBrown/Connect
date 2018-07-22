using Connect.Core.Common;
using Connect.Core.DomainEvents;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Connect.Core.Models
{
    public class User : AggregateRoot
    {
        public User(string username, byte[] salt, string password)
            => Apply(new UserCreated(username, salt, password));

        public System.Guid UserId { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public string Password { get; set; }
        public byte[] Salt { get; set; }
        public ICollection<UserRole> UserRoles { get; set; }
        = new HashSet<UserRole>();

        public void SignOut() => Apply(new UserSignedOut());

        public void SignIn(string hashedPassword) {
            if (Password != hashedPassword) throw new System.Exception();

            Apply(new UserSignedIn());
        }

        protected override void EnsureValidState()
        {
            
        }

        protected override void When(DomainEvent @event)
        {
            switch(@event)
            {
                case UserCreated userCreated:
                    Username = userCreated.Username;
                    Salt = userCreated.Salt;
                    Password = userCreated.Password;
                    break;
                case UserSignedOut userSignedOut:
                    break;

                case UserSignedIn userSignedIn:
                    break;
            }
        }
    }
}
