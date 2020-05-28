using UserManagement.Domain.Entities;
using UserManagement.Domain.Repositories;
using UserManagement.Infrastructure.Context;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Infrastructure.Repositories
{
    public class GroupRepository : IGroupRepository
    {
        private readonly UserDataContext _userDataContext;

        public GroupRepository(UserDataContext userDataContext)
        {
            _userDataContext = userDataContext;
        }

        public async Task CreateAsync(Group group)
        {
            await _userDataContext.Groups.InsertOneAsync(group);
        }

        public async Task<Group> GetAsync(string id)
        {
            var filter = Builders<Group>.Filter.Eq("Id", id);
            return await _userDataContext.Groups.Find(filter).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Group>> GetAsync()
        {
            return await _userDataContext.Groups.AsQueryable().ToListAsync();
        }

        public async Task UpdateAsync(string id, Group group)
        {
            var filter = Builders<Group>.Filter.Eq("Id", id);
            var updateDefinition = Builders<Group>
                .Update
                .Set(g => g.Name, group.Name)
                .Set(g => g.PermissionModules, group.PermissionModules);

            await _userDataContext.Groups.UpdateOneAsync(filter, updateDefinition);
        }
    }
}
