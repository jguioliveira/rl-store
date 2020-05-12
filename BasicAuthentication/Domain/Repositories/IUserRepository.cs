using BasicAuthentication.Domain.Entities;
using System.Threading.Tasks;

namespace BasicAuthentication.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);
        Task CreateUserAsync(User user);
        Task<User> GetByEmail(string email);
    }
}