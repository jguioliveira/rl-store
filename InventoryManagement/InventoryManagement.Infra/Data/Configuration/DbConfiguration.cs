using InventoryManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InventoryManagement.Infrastructure.Data.Configuration
{
    public static class DbConfiguration
    {
        public static IServiceCollection ConfigureInventoryDb(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextPool<InventoryContext>(options => 
            {
                options.UseMySql(connectionString);
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            return services;
        }
    }
}
