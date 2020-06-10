namespace InventoryManagement.Domain.Commands
{
    public interface ICommandResult
    {
        bool Success { get; set; }
        object Message { get; set; }
    }
}
