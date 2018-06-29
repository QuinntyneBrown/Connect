using MediatR;

namespace Connect.Core.Identity
{
    public class FailedLoginInvalidUsernameOrPasswordEvent: INotification
    {
        public FailedLoginInvalidUsernameOrPasswordEvent(string username)
        {
            Username = username;
        }

        public string Username { get; set; }
    }
}
