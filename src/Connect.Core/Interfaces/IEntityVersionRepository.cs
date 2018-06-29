using Connect.Core.Models;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.Core.Interfaces
{
    public interface IEntityVersionRepository
    {
        EntityVersion Get(int entityId, string entityName);
        void Create(EntityVersion entityVersion);
        Task SaveChangesAsync(CancellationToken cancellationToken);
        void SaveChanges();
        Task Remove(EntityVersion entityVersion);
    }
}
