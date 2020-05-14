using BasicAuthentication.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicAuthentication.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string email);
        Task CreateUserAsync(User user);
        Task<User> GetByEmailAsync(string email);
        Task<IEnumerable<User>> GetAsync();
    }
}