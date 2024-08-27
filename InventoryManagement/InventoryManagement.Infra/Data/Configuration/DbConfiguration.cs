using InventoryManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace InventoryManagement.Infrastructure.Data.Configuration
{
    public static class DbConfiguration
    {
        public static IServiceCollection ConfigureInventoryDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<InventoryContext>(options => 
            {
                // Replace with your server version and type.
                // Use 'MariaDbServerVersion' for MariaDB.
                // Alternatively, use 'ServerVersion.AutoDetect(connectionString)'.
                // For common usages, see pull request #1233.
                var serverVersion = new MySqlServerVersion(new Version(8, 0, 36));

                options.UseMySql(connectionString, serverVersion);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            return services;
        }
    }
}
