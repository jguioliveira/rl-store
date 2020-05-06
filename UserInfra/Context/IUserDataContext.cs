using MongoDB.Driver;

namespace UserInfra.Context
{
    public interface IUserDataContext<TEntity> where TEntity : class
    {
        IMongoCollection<TEntity> DataCollection { get; }
    }
}