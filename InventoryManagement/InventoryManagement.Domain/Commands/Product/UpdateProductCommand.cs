namespace InventoryManagement.Domain.Commands
{
    public class UpdateProductCommand : CreateProductCommand
    {
        public string Id { get; set; }

        public override void Validate()
        {
            if (string.IsNullOrEmpty(Id))
                AddError("Invalid code.");

            base.Validate();
        }
    }
}
