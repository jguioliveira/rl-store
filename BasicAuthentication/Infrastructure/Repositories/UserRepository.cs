using BasicAuthentication.Domain.Entities;
using BasicAuthentication.Domain.Repositories;
using BasicAuthentication.Infrastructure.Context;
using MongoDB.Driver;
using System.Threading.Tasks;

namespace BasicAuthentication.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly IUserDataContext _dataContext;

        public UserRepository(IUserDataContext dataContext)
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
            await _dataContext.Users.InsertOneAsync(user);
        }

        public async Task<User> GetByEmail(string email)
        {
            var filter = Builders<User>.Filter.Eq("email", email);
            return await _dataContext.Users
                                 .Find(filter)
                                 .SingleOrDefaultAsync();

        }
    }
}