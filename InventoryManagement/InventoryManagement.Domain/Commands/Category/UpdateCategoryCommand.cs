using InventoryManagement.Domain.Validation;

namespace InventoryManagement.Domain.Commands
{
    public class UpdateCategoryCommand : Validate, ICommand
    {
        public string Id { get; set; }
        public string FatherCategoryId { get; set; }
        public string Name { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Name))
                AddError("Invalid name.");

            if (string.IsNullOrEmpty(Id))
                AddError("Invalid id.");
        }
    }
}
