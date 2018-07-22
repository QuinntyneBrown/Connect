using Connect.Core.Models;
using System;

namespace Connect.API.Features.Identity
{
    public class UserDto
    {
        public System.Guid UserId { get; set; }
        public string Username { get; set; }
        public static UserDto FromUser(User user)
            => new UserDto
            {
                UserId = user.UserId,
                Username = user.Username
            };
    }
}
