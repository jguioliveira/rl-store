using BasicAuthentication.Domain.Entities;
using BasicAuthentication.Domain.Repositories;
using BasicAuthentication.Infrastructure.Context;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAuthentication.Infrastructure.Repositories
{
    public class ModuleRepository : IModuleRepository
    {
        private readonly IUserDataContext _userDataContext;

        public ModuleRepository(IUserDataContext userDataContext)
        {
            _userDataContext = userDataContext;
        }

        public async Task CreateAsync(Module module)
        {
            await _userDataContext.Modules.InsertOneAsync(module);
        }

        public async Task<Module> GetAsync(string id)
        {
            var filter = Builders<Module>.Filter.Eq("id", id);
            return await _userDataContext.Modules.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Module>> GetAsync()
        {
            return await _userDataContext.Modules.AsQueryable().ToListAsync();
        }

        public async Task UpdateAsync(Module module)
        {
            var filter = Builders<Module>.Filter.Eq("id", module.Id);

            var updateDefinition = Builders<Module>.Update
                .Set("name", module.Name)
                .Set("active", module.Active);

            await _userDataContext.Modules.UpdateOneAsync(filter, updateDefinition);
        }
    }
}
