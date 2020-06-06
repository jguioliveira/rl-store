using InventoryManagement.Domain.Validation;

namespace InventoryManagement.Domain.Commands
{
    public class CreateManufacturerCommand : Validate, ICommand
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public void Validate()
        {
            if(string.IsNullOrEmpty(Name))
                AddError("Invalid Name.");
        }
    }
}
