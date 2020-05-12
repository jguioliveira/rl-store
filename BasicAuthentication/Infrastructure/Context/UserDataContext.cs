using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BasicAuthentication.Infrastructure.Context
{
    public class UserDataContext<TEntity> : IUserDataContext<TEntity> where TEntity : class
    {
        public IMongoCollection<TEntity> DataCollection { get; private set; }

        public UserDataContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            var database = client.GetDatabase(settings.Value.DatabaseName);
            DataCollection = database.GetCollection<TEntity>("User");
        }

    }
}