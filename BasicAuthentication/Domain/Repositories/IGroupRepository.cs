using BasicAuthentication.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicAuthentication.Domain.Repositories
{
    public interface IGroupRepository
    {
        Task<Group> GetAsync(string id);
        Task<IEnumerable<Group>> GetAsync();
        Task CreateAsync(Group group);
        Task UpdateAsync(Group group);
    }
}
