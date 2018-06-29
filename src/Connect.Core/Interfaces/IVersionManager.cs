using Connect.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.Core.Interfaces
{
    public interface IEntityVersionManager
    {
        EntityVersion Acquire(int entityId, string entityName, int entityVersion);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        Task Release(EntityVersion entityVersion);
    }
}
