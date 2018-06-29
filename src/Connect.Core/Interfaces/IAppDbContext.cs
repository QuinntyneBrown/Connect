using Connect.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.Core.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<AccessToken> AccessTokens { get; set; }
        DbSet<Conversation> Conversations { get; set; }
        DbSet<Card> Cards { get; set; }
        DbSet<CardLayout> CardLayouts { get; set; }
        DbSet<Dashboard> Dashboards { get; set; }
        DbSet<DashboardCard> DashboardCards { get; set; }
        DbSet<DigitalAsset> DigitalAssets { get; set; }
        DbSet<EntityVersion> EntityVersions { get; set; }                                        
        DbSet<Profile> Profiles { get; set; }
        DbSet<ProfileType> ProfileTypes { get; set; }
        DbSet<Report> Reports { get; set; }
        DbSet<User> Users { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
