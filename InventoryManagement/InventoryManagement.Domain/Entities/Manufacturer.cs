using System;

namespace InventoryManagement.Domain.Entities
{
    public class Manufacturer
    {
        public static Manufacturer New(string name)
        {
            return new Manufacturer(Guid.NewGuid().ToString(), name);
        }

        private Manufacturer() { }

        public Manufacturer(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public string Id { get; private set; }
        public string Name { get; private set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}
