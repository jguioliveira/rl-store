namespace InventoryManagement.Domain.Commands
{
    public class CommandResult : ICommandResult
    {
        public CommandResult()
        {
        }

        public CommandResult(bool success, object message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; set; }
        public object Message { get; set; }
    }
}
