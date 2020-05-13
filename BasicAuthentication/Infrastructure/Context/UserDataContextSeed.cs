using BasicAuthentication.Domain.Entities;
using BasicAuthentication.Domain.ValueObjects;
using MongoDB.Bson;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BasicAuthentication.Infrastructure.Context
{
    public class UserDataContextSeed
    {
        private static IUserDataContext _userDataContext;

        public static async Task SeedAsync(IUserDataContext userDataContext)
        {
            _userDataContext = userDataContext;

            if (!_userDataContext.Users.AsQueryable().Any())
            {
                await CreateModulesAsync();
            }

            //if (!_userDataContext.Modules.AsQueryable().Any())
            //{
            //    await CreateModulesAsync();
            //}
        }

        static async Task CreateModulesAsync()
        {
            IEnumerable<Module> modules = new List<Module>
            {
                new Module{ Name = "User Management"},
                new Module{ Name = "Account and Finance"},
                new Module{ Name = "Purchasing"}
            };

            await _userDataContext.Modules.InsertManyAsync(modules);
            await CreateGroupsAsync(modules);
        }

        static async Task CreateGroupsAsync(IEnumerable<Module> modules)
        {
            PermissionModule permissionModule = await CreatePermissionModule(modules.FirstOrDefault(m => m.Name == "User Management"));

            Group group = new Group
            {
                Name = "SuperUser",
                PermissionModules = new List<ObjectId>
                {
                    permissionModule.Id
                }
            };

            await _userDataContext.Groups.InsertOneAsync(group);

            await CreateUser(group);
        }

        static async Task<PermissionModule> CreatePermissionModule(Module module)
        {
            PermissionModule permissionModule = new PermissionModule
            {
                ModuleId = module.Id,
                Permission = new Permission
                {
                    Delete = true,
                    Insert = true,
                    Update = true,
                    Select = true,
                }
            };

            await _userDataContext.PermissionModules.InsertOneAsync(permissionModule);
            return permissionModule;
        }

        static async Task CreateUser(Group group)
        {
            User user = new User("admin@company.com", "Super", "Admin", "admin@15")
            {
                Groups = new List<ObjectId> { group.Id }
            };

            await _userDataContext.Users.InsertOneAsync(user);
        }
    }
}
