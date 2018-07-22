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
                if (context.Roles.FirstOrDefault(x => x.Name == "Admin") == null) {                    
                    context.Roles.Add(new Role()
                    {
                        Name = "Admin"
                    });
                }

                context.SaveChanges();
            }
        }

        internal class UserConfiguration
        {
            public static void Seed(AppDbContext context)
            {
                if (context.Users.FirstOrDefault(x => x.Username == "quinntynebrown@gmail.com") == null)
                {
                    var user = new User()
                    {
                        Username = "quinntynebrown@gmail.com"
                    };
                    user.Password = new PasswordHasher().HashPassword(user.Salt, "P@ssw0rd");

                    user.UserRoles.Add(new UserRole()
                    {
                        RoleId = context.Roles.Single(x => x.Name == "Admin").RoleId
                    });

                    context.Users.Add(user);
                }
                
                if (context.Users.FirstOrDefault(x => x.Username == "quinntyne@hotmail.com") == null)
                {
                    var user = new User()
                    {
                        Username = "quinntyne@hotmail.com"
                    };

                    user.UserRoles.Add(new UserRole()
                    {
                        RoleId = context.Roles.Single(x => x.Name == "Admin").RoleId
                    });

                    user.Password = new PasswordHasher().HashPassword(user.Salt, "P@ssw0rd");

                    context.Users.Add(user);
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