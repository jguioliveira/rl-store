using InventoryManagement.Domain.Validation;
using System;

namespace InventoryManagement.Domain.Commands
{
    public class UpdateManufacturerCommand : Validate, ICommand
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public void Validate()
        {
            if (string.IsNullOrEmpty(Id))
                AddError("Invalid Id.");

            if (string.IsNullOrEmpty(Name))
                AddError("Invalid Name.");
        }
    }
}
