using MongoDB.Driver;

namespace BasicAuthentication.Infrastructure.Context
{
    public interface IUserDataContext<TEntity> where TEntity : class
    {
        IMongoCollection<TEntity> DataCollection { get; }
    }
}