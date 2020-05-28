using System;
using UserManagement.Infrastructure.Context;
using UserManagement.Infrastructure.MongoDb;
using UserManagement.Infrastructure.MongoDb.Map;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class MongoDbServiceCollectionExtensions
    {
        public static void ConfigureMongoDb(this IServiceCollection services, Action<MongoDbOptions> options)
        {
            ModuleMap.Configure();
            GroupMap.Configure();
            UserMap.Configure();

            MongoDbOptions mongoDbOptions = new MongoDbOptions();
            options.Invoke(mongoDbOptions);

            var userDataContext = UserDataContext.Create(mongoDbOptions.ConnectionString, mongoDbOptions.DatabaseName);

            UserDataContextSeed.SeedAsync(userDataContext).Wait();

            services.AddSingleton(userDataContext);            
        }
    }
}
