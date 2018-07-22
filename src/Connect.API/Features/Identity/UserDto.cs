using Connect.Core.Models;

namespace Connect.API.Features.Identity
{
    public class UserDto
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public static UserDto FromUser(User user)
            => new UserDto
            {
                UserId = user.UserId,
                Username = user.Username
            };
    }
}
