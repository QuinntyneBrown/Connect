using Connect.Core.Models;
using Connect.Core.Identity;
using Connect.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Connect.API
{
    public class SeedData
    {
        public static void Seed(AppDbContext context)
        {
            ProfileTypeConfiguration.Seed(context);

            UserConfiguration.Seed(context);
            
            context.SaveChanges();
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

                    context.Users.Add(user);
                }
                
                if (context.Users.FirstOrDefault(x => x.Username == "quinntyne@hotmail.com") == null)
                {
                    var user = new User()
                    {
                        Username = "quinntyne@hotmail.com"
                    };
                    user.Password = new PasswordHasher().HashPassword(user.Salt, "P@ssw0rd");

                    context.Users.Add(user);
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


                context.SaveChanges();
            }
        }
    }
}