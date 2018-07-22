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

        public DbSet<StoredEvent> StoredEvents { get; set; }
        public DbSet<AccessToken> AccessTokens { get; set; }
        public DbSet<Conversation> Conversations { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardLayout> CardLayouts { get; set; }
        public DbSet<ContactRequest> ContactRequests { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Dashboard> Dashboards { get; set; }
        public DbSet<DashboardCard> DashboardCards { get; set; }
        public DbSet<StoredEvent> DomainEvents { get; set;  }
        public DbSet<DigitalAsset> DigitalAssets { get; set; }
        public DbSet<EntityVersion> EntityVersions { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
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
            
            var domainEventEntities = ChangeTracker.Entries<AggregateRoot>()
                .Select(entityEntry => entityEntry.Entity)
                .Where(entity => entity.DomainEvents.Any())
                .ToArray();
            
            result = await base.SaveChangesAsync(cancellationToken);

            foreach (var entity in domainEventEntities)
            {
                var events = entity.DomainEvents
                    .ToArray();

                entity.ClearEvents();

                foreach (var @event in events)
                {                    
                    await _mediator.Publish(@event, cancellationToken);
                }

                base.SaveChanges();
            }

            return result;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {            
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