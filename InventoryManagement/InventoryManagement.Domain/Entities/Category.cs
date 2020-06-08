using System;
using System.Collections.Generic;

namespace InventoryManagement.Domain.Entities
{
    public class Category
    {
        public static int MAX_SUBCATEGORIES { get => 2; }

        public static Category New(string name)
        {
            return New(name, null);
        }

        public static Category New(string name, string fatherCategoryId)
        {
            return new Category(Guid.NewGuid().ToString(), name, fatherCategoryId);
        }

        private Category() { }

        public Category(string id, string name, string fatherCategoryId)
        {
            Id = id;
            Name = name;
            FatherCategoryId = fatherCategoryId;
        }

        public string Id { get; private set; }
        public string FatherCategoryId { get; private set; }
        public string Name { get; private set; }


        public IEnumerable<Product> Products { get; internal set; }
    }
}
