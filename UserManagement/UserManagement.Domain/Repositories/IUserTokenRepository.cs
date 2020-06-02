using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories
{
    public interface IUserTokenRepository
    {
        Task CreateAsync(UserToken userToken);
        Task<UserToken> GetAsync(string authorizationCode);
        Task UpdateAsync(string id, UserToken userToken);
    }
}
