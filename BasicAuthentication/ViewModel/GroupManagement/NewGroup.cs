using System.Collections.Generic;

namespace BasicAuthentication.ViewModel.GroupManagement
{
    public class NewGroup
    {
        public string Name { get; set; }

        public IList<PermissionModuleData> PermissionModules { get; set; }
    }
}
