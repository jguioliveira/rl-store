namespace UserManagement.Domain.Entities
{
    public class Module
    {
        public Module(string name, bool active)
        {
            Name = name;
            Active = active;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public bool Active { get; private set; }
    }
}
