using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Connect.Core.Interfaces;
using Connect.Infrastructure.Data;

namespace Connect.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {                
        public static IServiceCollection AddDataStore(this IServiceCollection services,
                                               string connectionString, bool useInMemoryDatabase = false)
        {
            services.AddTransient<IAppDbContext, AppDbContext>();
            services.AddTransient<IAccessTokenRepository, AccessTokenRepository>();

            if (useInMemoryDatabase) {
                services.AddDbContext<AppDbContext>(options =>
                {
                    options
                    .UseLoggerFactory(AppDbContext.ConsoleLoggerFactory)
                    .UseInMemoryDatabase(databaseName: $"InMemoryDatabase");
                });

                return services;
            }
            services.AddDbContext<AppDbContext>(options =>
            {                
                options
                .UseLoggerFactory(AppDbContext.ConsoleLoggerFactory)
                .UseSqlServer(connectionString, b=> {
                    b.MigrationsAssembly("Connect.Infrastructure");
                    b.EnableRetryOnFailure();
                });
            });

            return services;
        }
    }
}
