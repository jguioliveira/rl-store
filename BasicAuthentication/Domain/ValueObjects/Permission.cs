namespace BasicAuthentication.Domain.ValueObjects
{
    public class Permission
    {
        public bool Insert { get; set; }
        public bool Update { get; set; }
        public bool Delete { get; set; }
        public bool Select { get; set; }
    }
}
