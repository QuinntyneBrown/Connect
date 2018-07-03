using Connect.Core.Common;
using Connect.Core.Interfaces;
using Connect.Core.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Connect.Infrastructure.Data
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        private readonly IMediator _mediator;
        public AppDbContext(DbContextOptions options, IMediator mediator = default(IMediator))
            :base(options) {
            _mediator = mediator;
        }

        public static readonly LoggerFactory ConsoleLoggerFactory
            = new LoggerFactory(new[] {
                new ConsoleLoggerProvider((category, level)
                    => category == DbLoggerCategory.Database.Command.Name 
                && level == LogLevel.Information, true) });

        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardLayout> CardLayouts { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<DashboardCard> DashboardCards { get; set; }
        public DbSet<DomainEvent> DomainEvents { get; set;  }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        public DbSet<EntityVersion> EntityVersions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<ProfileType> ProfileTypes { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ServiceProvider> ServiceProviders { get; set; }
        public DbSet<User> Users { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            var result = default(int);
            
            var domainEventEntities = ChangeTracker.Entries<Entity>()
                .Select(entityEntry => entityEntry.Entity)
                .Where(entity => entity.DomainEvents.Any())
                .ToArray();
            
            foreach (var entity in ChangeTracker.Entries<Entity>()
                .Where(e => (e.State == EntityState.Added || (e.State == EntityState.Modified)))
                .Select(x => x.Entity))
            {
                var isNew = entity.CreatedOn == default(DateTime);
                entity.CreatedOn = isNew ? DateTime.UtcNow : entity.CreatedOn;   
                entity.LastModifiedOn = DateTime.UtcNow;
            }

            foreach (var item in ChangeTracker.Entries<Entity>().Where(e => e.State == EntityState.Deleted))
            {
                item.State = EntityState.Modified;
                item.Entity.IsDeleted = true;
            }

            result = await base.SaveChangesAsync(cancellationToken);

            foreach (var entity in domainEventEntities)
            {
                var events = entity.DomainEvents
                    .ToArray();

                entity.ClearEvents();

                foreach (var @event in events)
                {
                    @event.Payload = JsonConvert.SerializeObject(@event.EventData);

                    DomainEvents.Add(@event);

                    await _mediator.Publish(@event, cancellationToken);
                }

                base.SaveChanges();
            }

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Card>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<CardLayout>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Conversation>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<ContactRequest>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Dashboard>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<DashboardCard>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<DigitalAsset>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Order>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Product>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Profile>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Report>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<User>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<EntityVersion>()
                .HasKey(e => new { e.EntityId, e.Version, e.EntityName });

            modelBuilder.Entity<OrderItem>()
                .HasKey(e => new { e.OrderId, e.ProductId });

            modelBuilder.Entity<UserRole>()
                .HasKey(e => new { e.UserId, e.RoleId });

            base.OnModelCreating(modelBuilder);
        }       
    }
}