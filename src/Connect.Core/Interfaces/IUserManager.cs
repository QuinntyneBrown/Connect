using System.Threading.Tasks;

namespace Connect.Core.Interfaces
{
    public interface IUserManager
    {
        Task<bool> IsLockedOutAsync(string username);
    }
}
