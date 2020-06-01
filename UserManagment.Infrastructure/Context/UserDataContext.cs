using UserManagement.Domain.Entities;
using MongoDB.Driver;

namespace UserManagement.Infrastructure.Context
{
    public class UserDataContext
    {
        private IMongoDatabase _database = null;
        private static UserDataContext _userDataContext;

        private UserDataContext() { }

        public static UserDataContext Create(string connectionString, string databaseName)
        {
            if (_userDataContext is null)
            {
                _userDataContext = new UserDataContext();

                var client = new MongoClient(connectionString);

                _userDataContext._database = client.GetDatabase(databaseName);
            }

            return _userDataContext;
        }

        public IMongoCollection<User> Users => _database.GetCollection<User>(nameof(User));

        public IMongoCollection<UserToken> UserTokens => _database.GetCollection<UserToken>(nameof(UserToken));

        public IMongoCollection<Module> Modules => _database.GetCollection<Module>(nameof(Module));

        public IMongoCollection<Group> Groups => _database.GetCollection<Group>(nameof(Group));
    }
}