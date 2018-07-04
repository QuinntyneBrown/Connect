using System.Threading.Tasks;
using Connect.Core.Interfaces;

namespace Connect.Core.Identity
{
    public class UserManager : IUserManager
    {
        public Task<bool> IsLockedOutAsync(string username)
        {
            throw new System.NotImplementedException();
        }
    }
}
