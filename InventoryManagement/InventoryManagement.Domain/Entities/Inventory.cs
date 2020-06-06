using System;

namespace InventoryManagement.Domain.Entities
{
    public class Inventory
    {
        public Inventory(Product product, short count)
        {
            Product = product;
            Count = count;
            MinCount = 0;
            CreatedOn = DateTime.Now;
            LastChange = DateTime.Now;
        }

        public Product Product { get; private set; }
        public short Count { get; private set; }
        public byte MinCount { get; private set; }
        public DateTime CreatedOn { get; private set; }
        public DateTime LastChange { get; private set; }

        public void Add(short count)
        {
            Count += count;
            LastChange = DateTime.Now;
        }

        public void Subtract(short count)
        {
            if (count > Count)
                throw new ArgumentOutOfRangeException();

            Count -= count;
            LastChange = DateTime.Now;
        }

        public void UpdateMinCount(byte minCount)
        {
            MinCount = minCount;
            LastChange = DateTime.Now;
        }

        public void UpdateCount(short count)
        {
            Count = count;
            LastChange = DateTime.Now;
        }
    }
}
