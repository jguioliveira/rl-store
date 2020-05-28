using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Context;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly UserDataContext _dataContext;

        public UserRepository(UserDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> UserExistsAsync(string email)
        {
            var user = await GetAsync(email);
            return !(user is null);
        }

        public async Task CreateUserAsync(User user)
        {
            await _dataContext.Users.InsertOneAsync(user);
        }

        public async Task<User> GetAsync(string email)
        {
            var filter = Builders<User>.Filter.Eq("email", email);
            return await _dataContext.Users
                                 .Find(filter)
                                 .SingleOrDefaultAsync();

        }

        public async Task<IEnumerable<User>> GetAsync()
        {
            return await _dataContext.Users.AsQueryable().ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            var filter = Builders<User>.Filter.Eq("Id", id);
            return await _dataContext.Users
                                 .Find(filter)
                                 .SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, User user)
        {
            var filter = Builders<User>.Filter.Eq("Id", id);
            var updateDefinition = Builders<User>
                .Update
                .Set(u => u.FirstName, user.FirstName)
                .Set(u => u.LastName, user.LastName)
                .Set(u => u.Password, user.Password)
                .Set(u => u.Active, user.Active);

            await _dataContext.Users.UpdateOneAsync(filter, updateDefinition);
        }
    }
}