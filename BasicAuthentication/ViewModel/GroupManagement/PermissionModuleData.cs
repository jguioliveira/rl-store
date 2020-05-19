namespace BasicAuthentication.ViewModel.GroupManagement
{
    public class PermissionModuleData
    {
        public string ModuleId { get; set; }
        public string ModuleName { get; set; }
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Select { get; set; }
    }
}
