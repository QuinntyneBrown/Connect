using Connect.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.Core.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<EntityVersion> EntityVersions { get; set; }        
        DbSet<AccessToken> AccessTokens { get; set; }        
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
