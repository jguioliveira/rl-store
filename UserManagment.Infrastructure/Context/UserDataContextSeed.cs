using UserManagement.Domain.Entities;
using UserManagement.Domain.ValueObjects;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Infrastructure.Context
{
    public class UserDataContextSeed
    {
        private static UserDataContext _userDataContext;

        public static async Task SeedAsync(UserDataContext userDataContext)
        {
            _userDataContext = userDataContext;

            if (!_userDataContext.Users.AsQueryable().Any())
            {
                await CreateModulesAsync();
            }
        }

        static async Task CreateModulesAsync()
        {
            IEnumerable<Module> modules = new List<Module>
            {
                new Module("User Management", true),
                new Module("Account and Finance", true),
                new Module("Purchasing", true)
            };

            await _userDataContext.Modules.InsertManyAsync(modules);
            await CreateGroupsAsync(modules);
        }

        static async Task CreateGroupsAsync(IEnumerable<Module> modules)
        {
            PermissionModule permissionModule = CreatePermissionModule(modules.FirstOrDefault(m => m.Name == "User Management"));

            Group group = new Group("SuperUser");
            group.AddPermissionModule(permissionModule);

            await _userDataContext.Groups.InsertOneAsync(group);

            await CreateUser(group);
        }

        static PermissionModule CreatePermissionModule(Module module)
        {
            PermissionModule permissionModule = new PermissionModule (
                module.Id,
                new Permission(true, true, true, true)
            );

            return permissionModule;
        }

        static async Task CreateUser(Group group)
        {
            User user = new User("admin@company.com", "Super", "Admin", "admin@15", true);

            user.AddGroup(group.Id);

            await _userDataContext.Users.InsertOneAsync(user);
        }
    }
}
