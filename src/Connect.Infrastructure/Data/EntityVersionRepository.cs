using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Connect.Core.Extensions;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.Infrastructure.Data
{
    public class EntityVersionRepository: IEntityVersionRepository
    {
        private readonly IAppDbContext _context;


        public EntityVersionRepository(IAppDbContext context)
        {
            _context = context;
        }

        public EntityVersion Get(int entityId, string entityName)
            => _context.EntityVersions
            .Where(x => x.EntityId == entityId && x.EntityName == entityName)
            .OrderByDescending(x => x.Version)
            .FirstOrDefault();

        public void Create(EntityVersion entityVersion)
            => _context.EntityVersions.Add(entityVersion);

        public async Task SaveChangesAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public void SaveChanges()
        {
            _context.SaveChangesAsync(default(CancellationToken)).GetAwaiter().GetResult();
        }

        public Task Remove(EntityVersion entityVersion)
        {
            _context.EntityVersions.Remove(entityVersion);
            return Task.CompletedTask;
        }
    }
}
