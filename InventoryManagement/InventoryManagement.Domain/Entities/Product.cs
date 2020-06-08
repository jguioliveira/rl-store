using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class Product
    {
        public static Product New(string name, string categoryId, string manufacturerId)
        {
            var product = new Product(Guid.NewGuid().ToString(), name, categoryId, manufacturerId);
            return product;
        }

        private Product() { }

        public Product(string id, string name, string categoryId, string manufacturerId)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            ManufacturerId = manufacturerId;
        }

        public string Id { get; private set; }
        public string Code { get; set; }
        public string Name { get; private set; }

        public string CategoryId { get; private set; }
        public string ManufacturerId { get; private set; }

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

            ProductBookMark productBookMark = new ProductBookMark(Id, code, name, value, count);

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
                Inventory = new Inventory(Id, count);

            Inventory.UpdateMinCount(minCount);
            Inventory.UpdateCount(count);
        }
    }
}
