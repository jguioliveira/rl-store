using InventoryManagement.Domain.Commands;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Handlers
{
    public interface IHandler<T> where T : ICommand
    {
        ICommandResult Handle(T command);

        Task<ICommandResult> HandleAsync(T command);
    }
}
