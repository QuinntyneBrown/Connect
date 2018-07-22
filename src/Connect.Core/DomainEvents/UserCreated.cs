namespace Connect.Core.DomainEvents
{
    public class UserCreated: DomainEvent
    {
        public UserCreated(string username, byte[] salt, string password)
        {
            Username = username;
            Password = password;
        }
        public byte[] Salt { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
