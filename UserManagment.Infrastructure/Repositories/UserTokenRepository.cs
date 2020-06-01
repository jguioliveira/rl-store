using MongoDB.Driver;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Context;

namespace UserManagement.Infrastructure.Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly UserDataContext _dataContext;

        public UserTokenRepository(UserDataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task CreateAsync(UserToken userToken)
        {
            await _dataContext.UserTokens.InsertOneAsync(userToken);
        }

        public async Task<UserToken> GetAsync(string authorizationCode)
        {
            var filter = Builders<UserToken>.Filter.Eq("AuthorizationCode", authorizationCode);
            return await _dataContext.UserTokens
                                 .Find(filter)
                                 .SingleOrDefaultAsync();
        }

        public async Task UpdateAsync(string id, UserToken userToken)
        {
            var filter = Builders<UserToken>.Filter.Eq("Id", id);
            var updateDefinition = Builders<UserToken>
                .Update
                .Set(u => u.Token, userToken.Token)
                .Set(u => u.TokenExpiration, userToken.TokenExpiration)
                .Set(u => u.RefreshToken, userToken.RefreshToken)
                .Set(u => u.IsCanceled, userToken.IsCanceled);

            await _dataContext.UserTokens.UpdateOneAsync(filter, updateDefinition);
        }
    }
}
