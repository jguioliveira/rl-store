using System.Collections.Generic;
using System.Linq;
using UserManagement.Domain.ValueObjects;

namespace UserManagement.Domain.Entities
{
    public class Group
    {
        private List<PermissionModule> _permissionModules;

        public Group(string name)
        {
            Name = name;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public IReadOnlyCollection<PermissionModule> PermissionModules { get { return _permissionModules.AsReadOnly(); } private set { _permissionModules = value.ToList(); } }

        public void AddPermissionModule(PermissionModule permissionModule)
        {
            if(_permissionModules is null)
            {
                _permissionModules = new List<PermissionModule>();
            }

            _permissionModules.Add(permissionModule);
        }

        public void AddPermissionModule(string moduleId, bool insert, bool update, bool delete, bool select)
        {
            var permissionModule = new PermissionModule(moduleId, insert, update, delete, select);

            if (_permissionModules is null)
            {
                _permissionModules = new List<PermissionModule>();
            }

            _permissionModules.Add(permissionModule);
        }

        public void AddPermissionModuleRange(IEnumerable<PermissionModule> permissionModules)
        {
            if (_permissionModules is null)
            {
                _permissionModules = new List<PermissionModule>();
            }

            _permissionModules.AddRange(permissionModules);
        }
    }
}
