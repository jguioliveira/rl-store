using System.Threading.Tasks;
using UserDomain.Entities;

namespace UserDomain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);
        Task CreateUserAsync(User user);
        Task<User> GetByEmail(string email);
    }
}