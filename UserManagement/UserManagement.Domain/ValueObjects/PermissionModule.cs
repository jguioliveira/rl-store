namespace UserManagement.Domain.ValueObjects
{
    public class PermissionModule
    {
        public PermissionModule(string moduleId, Permission permission)
        {
            ModuleId = moduleId;
            Permission = permission;
        }

        public PermissionModule(string moduleId, bool insert, bool update, bool delete, bool select)
        {
            ModuleId = moduleId;
            Permission = new Permission(insert, update, delete, select);
        }

        public string ModuleId { get; private set; }

        public Permission Permission { get; private set; }
    }
}
