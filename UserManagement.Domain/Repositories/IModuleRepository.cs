using BasicAuthentication.Domain.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BasicAuthentication.Domain.Repositories
{
    public interface IModuleRepository
    {

        Task<Module> GetAsync(string id);

        Task<IEnumerable<Module>> GetAsync();

        Task CreateAsync(Module module);

        Task UpdateAsync(string id, Module module);
    }
}
