using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class Product
    {
        public static Product New(string name, Category category, Manufacturer manufacturer)
        {
            var product = new Product(Guid.NewGuid().ToString(), name, category, manufacturer);
            return product;
        }

        private Product() { }

        public Product(string id, string name, Category category, Manufacturer manufacturer)
        {
            Id = id;
            Name = name;
            Category = category;
            Manufacturer = manufacturer;
        }

        public string Id { get; private set; }
        public string Code { get; set; }
        public string Name { get; private set; }
        public Category Category { get; private set; }
        public Manufacturer Manufacturer { get; private set; }
        public Inventory Inventory { get; private set; }

        private List<ProductBookMark> productBookMarks;
        public IReadOnlyCollection<ProductBookMark> ProductBookMarks
        {
            get { return productBookMarks; }
        }

        public void AddProductBookMarks(string code, string name, string value, short count)
        {
            if (productBookMarks is null)
                productBookMarks = new List<ProductBookMark>();

            ProductBookMark productBookMark = new ProductBookMark(this, code, name, value, count);

            productBookMarks.Add(productBookMark);
        }

        public void ClearProductBookMarks()
        {
            if (!(productBookMarks is null))
                productBookMarks.Clear();
        }

        public void UpdateInventory(short count, byte minCount)
        {
            if (Inventory is null)
                Inventory = new Inventory(this, count);

            Inventory.UpdateMinCount(minCount);
            Inventory.UpdateCount(count);
        }
    }
}
