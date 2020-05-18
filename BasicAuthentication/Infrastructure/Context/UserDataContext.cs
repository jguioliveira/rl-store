using BasicAuthentication.Domain.Entities;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace BasicAuthentication.Infrastructure.Context
{
    public class UserDataContext : IUserDataContext
    {
        private readonly IMongoDatabase _database = null;

        public UserDataContext(IOptions<DatabaseSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            _database = client.GetDatabase(settings.Value.DatabaseName);
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>(nameof(User));

        public IMongoCollection<Module> Modules => _database.GetCollection<Module>(nameof(Module));

        public IMongoCollection<Group> Groups => _database.GetCollection<Group>(nameof(Group));
    }
}