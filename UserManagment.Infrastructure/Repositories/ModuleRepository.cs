using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Context;
using UserManagement.Domain.Entities;

namespace UserManagement.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly UserDataContext _userDataContext;

        public ModuleRepository(UserDataContext userDataContext)
        {
            _userDataContext = userDataContext;
        }

        public async Task CreateAsync(Module module)
        {
            var dbModule = new Module(module.Name, module.Active);
            await _userDataContext.Modules.InsertOneAsync(dbModule);
        }

        public async Task<Module> GetAsync(string id)
        {
            var filter = Builders<Module>.Filter.Eq("Id", id);
            return await _userDataContext.Modules.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Module>> GetAsync()
        {
            return await _userDataContext.Modules.AsQueryable().ToListAsync();
        }

        public async Task UpdateAsync(string id, Module module)
        {
            var filter = Builders<Module>.Filter.Eq("Id", id);

            var updateDefinition = Builders<Module>.Update
                .Set("name", module.Name)
                .Set("active", module.Active);

            await _userDataContext.Modules.UpdateOneAsync(filter, updateDefinition);
        }
    }
}
