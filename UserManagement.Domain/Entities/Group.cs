using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.Entities
{
    public class Group
    {
        public Group(string name)
        {
            Name = name;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; }

        [BsonElement("name")]
        public string Name { get; private set; }

        [BsonElement("permissionModules")]
        private Collection<PermissionModule> permissionModules;

        public IReadOnlyCollection<PermissionModule> PermissionModules { get { return permissionModules.ToArray(); } }


        public void AddPermissionModule(PermissionModule permissionModule)
        {
            if(permissionModules is null)
            {
                permissionModules = new Collection<PermissionModule>();
            }

            permissionModules.Add(permissionModule);
        }

        public void AddPermissionModule(string moduleId, bool insert, bool update, bool delete, bool select)
        {
            var permissionModule = new PermissionModule(moduleId, insert, update, delete, select);

            if (permissionModules is null)
            {
                permissionModules = new Collection<PermissionModule>();
            }

            permissionModules.Add(permissionModule);
        }

        public void AddPermissionModuleRange(IEnumerable<PermissionModule> permissionModule)
        {
            if (permissionModules is null)
            {
                permissionModules = new Collection<PermissionModule>();
            }

            foreach (var item in permissionModule)
            {
                permissionModules.Add(item);
            }            
        }
    }
}
