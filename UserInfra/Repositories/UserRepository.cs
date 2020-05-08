using MongoDB.Driver;
using System.Threading.Tasks;
using UserDomain.Entities;
using UserDomain.Repositories;
using UserInfra.Context;

namespace UserInfra.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IUserDataContext<User> _dataContext;

        public UserRepository(IUserDataContext<User> dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> UserExists(string email)
        {
            var user = await GetByEmail(email);
            return !(user is null);
        }

        public async Task CreateUserAsync(User user)
        {
            await _dataContext.DataCollection.InsertOneAsync(user);
        }

        public async Task<User> GetByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq("email", email);
            return await _dataContext.DataCollection
                                 .Find(filter)
                                 .SingleOrDefaultAsync();

        }
    }
}