using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<bool> UserExistsAsync(string email);
        Task CreateUserAsync(User user);
        Task<User> GetAsync(string email);
        Task<IEnumerable<User>> GetAsync();
        Task<User> GetByIdAsync(string id);
        Task UpdateAsync(string id, User user);
    }
}