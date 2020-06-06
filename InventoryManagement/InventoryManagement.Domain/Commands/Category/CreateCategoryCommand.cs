using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Validation;
using System.Collections.Generic;
using System.Linq;

namespace InventoryManagement.Domain.Commands
{
    public class CreateCategoryCommand : Validate, ICommand
    {
        public int MAX_SUBCATEGORIES { get => Category.MAX_SUBCATEGORIES; }

        public class CategoryData
        {
            public string Name { get; set; }

            public IEnumerable<CategoryData> Children { get; set; }
        }

        public string Name { get; set; }

        public IEnumerable<CategoryData> Children { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                AddError("Invalid name.");

            static bool childrenNameEmpty(IEnumerable<CategoryData> categories)
            {
                bool result = categories.Any(c => string.IsNullOrEmpty(c.Name));

                if (!result)
                {
                    result = childrenNameEmpty(categories.SelectMany(c => c.Children));
                }

                return result;
            }

            if (!(Children is null) && childrenNameEmpty(Children))
                AddError("Invalid children name.");
        }
    }
}
