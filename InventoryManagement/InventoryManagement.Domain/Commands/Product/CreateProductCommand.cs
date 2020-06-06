using InventoryManagement.Domain.Validation;

namespace InventoryManagement.Domain.Commands
{
    public class CreateProductCommand : Validate, ICommand
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public string CategoryId { get; set; }
        public string ManufacturerId { get; set; }

        public virtual void Validate()
        {
            if (string.IsNullOrEmpty(Code))
                AddError("Invalid code.");

            if (string.IsNullOrEmpty(Name))
                AddError("Invalid name.");

            if (string.IsNullOrEmpty(CategoryId))
                AddError("Invalid category.");

            if (string.IsNullOrEmpty(ManufacturerId))
                AddError("Invalid manufacturer.");
        }
    }
}
