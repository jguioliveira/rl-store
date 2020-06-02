using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories
{
    public interface IGroupRepository
    {
        Task<Group> GetAsync(string id);
        Task<IEnumerable<Group>> GetAsync();
        Task CreateAsync(Group group);
        Task UpdateAsync(string id, Group group);
    }
}
