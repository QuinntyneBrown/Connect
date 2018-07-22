using Connect.Core.Models;
using Connect.Core.Identity;
using Connect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Connect.Core.Common;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;

namespace Connect.API
{
    public class AppInitializer: IDesignTimeDbContextFactory<AppDbContext>
    {
        public static void Seed(AppDbContext context)
        {
            ProfileTypeConfiguration.Seed(context);
            RoleConfiguration.Seed(context);
            UserConfiguration.Seed(context);
            OrderStatusesConfiguration.Seed(context);
            ProductConfiguration.Seed(context);
            context.SaveChanges();
        }

        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .AddUserSecrets(typeof(Startup).GetTypeInfo().Assembly)
                .Build();

            return new AppDbContext(new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer(configuration["Data:DefaultConnection:ConnectionString"])
                .Options);
        }

        internal class RoleConfiguration {
            public static void Seed(AppDbContext context)
            {
                var eventStore = new EventStore(context);

                if (eventStore.Query<Role>("Name", "Admin") == null)
                    eventStore.Save(new Role("Admin"));

                context.SaveChanges();
            }
        }

        internal class UserConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                var eventStore = new EventStore(context);

                if (eventStore.Query<User>("Username", "quinntynebrown@gmail.com") == null)
                {
                    var salt = new byte[128 / 8];
                    using (var rng = RandomNumberGenerator.Create())
                    {
                        rng.GetBytes(salt);
                    }

                    var user = new User(
                        "quinntynebrown@gmail.com",
                        salt,
                        new PasswordHasher().HashPassword(salt, "P@ssw0rd")
                        );

                    eventStore.Save(user);
                }

                context.SaveChanges();
            }
        }

        internal class OrderStatusesConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                if (context.OrderStatuses.FirstOrDefault(x => x.Name == nameof(OrderStatuses.AwaitingPayment)) == null)
                {
                    context.OrderStatuses.Add(new OrderStatus()
                    {

                        Name = nameof(OrderStatuses.AwaitingPayment)
                    });
                }
                context.SaveChanges();
            }
        }

        internal class ProfileTypeConfiguration
        {
            public static void Seed(AppDbContext context)
            {

                if (context.ProfileTypes.FirstOrDefault(x => x.Name == "Customer") == null)
                {
                    var profileType = new ProfileType()
                    {
                        Name = "Customer"
                    };

                    context.ProfileTypes.Add(profileType);
                }

                if (context.ProfileTypes.FirstOrDefault(x => x.Name == "ServiceProvider") == null)
                {
                    var profileType = new ProfileType()
                    {
                        Name = "ServiceProvider"
                    };
                    
                    context.ProfileTypes.Add(profileType);
                }
                
                context.SaveChanges();
            }
        }

        internal class ProductConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                if(context.Products.FirstOrDefault(x => x.Name == "100 Credits") == null)
                {
                    context.Products.Add(new Product()
                    {
                        Name = "100 Credits",
                        Description = "100 Credits"
                    });
                }

                context.SaveChanges();
            }
        }
    }
}