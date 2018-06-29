using Connect.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.Core.Interfaces
{
    public interface IAccessTokenRepository
    {
        Task<List<string>> GetValidAccessTokenValuesAsync();
        IQueryable<AccessToken> GetValidAccessTokens();
        Task<List<AccessToken>> GetValidTokensByUsernameAsync(string username);
        Task InvalidateByUsernameAsync(string username);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        void Add(AccessToken accessToken);
    }
}
