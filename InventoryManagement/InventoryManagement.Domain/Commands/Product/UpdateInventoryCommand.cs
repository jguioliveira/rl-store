using InventoryManagement.Domain.Validation;

namespace InventoryManagement.Domain.Commands
{
    public class UpdateInventoryCommand : Validate, ICommand
    {
        public string ProductId { get; set; }
        public short Count { get; set; }
        public byte MinCount { get; set; }

        public void Validate()
        {
            if (Count < 0)
                AddError("Invalid count.");

            if (MinCount < 0)
                AddError("Invalid minimum count.");
        }
    }
}
