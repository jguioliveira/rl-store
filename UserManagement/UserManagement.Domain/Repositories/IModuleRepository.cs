using System.Collections.Generic;
using System.Threading.Tasks;
using UserManagement.Domain.Entities;

namespace UserManagement.Domain.Repositories
{
    public interface IModuleRepository
    {

        Task<Module> GetAsync(string id);

        Task<IEnumerable<Module>> GetAsync();

        Task CreateAsync(Module module);

        Task UpdateAsync(string id, Module module);
    }
}
