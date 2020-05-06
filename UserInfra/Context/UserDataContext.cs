using MongoDB.Driver;

namespace UserInfra.Context
{
    public class UserDataContext<TEntity> : IUserDataContext<TEntity> where TEntity : class
    {
        public IMongoCollection<TEntity> DataCollection { get; private set; }

        public UserDataContext(IDatabaseSettings settings)
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);
            DataCollection = database.GetCollection<TEntity>(nameof(TEntity));
        }

    }
}