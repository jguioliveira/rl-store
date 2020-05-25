namespace BasicAuthentication.Domain.ValueObjects
{
    public class Permission
    {
        public Permission(bool insert, bool update, bool delete, bool select)
        {
            Insert = insert;
            Update = update;
            Delete = delete;
            Select = select;
        }

        public bool Insert { get; private set; }
        public bool Update { get; private set; }
        public bool Delete { get; private set; }
        public bool Select { get; private set; }
    }
}
