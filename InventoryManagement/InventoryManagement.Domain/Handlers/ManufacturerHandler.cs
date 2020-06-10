using InventoryManagement.Domain.Commands;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repositories;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Handlers
{
    public class ManufacturerHandler : 
        IHandler<CreateManufacturerCommand>, 
        IHandler<UpdateManufacturerCommand>
    {
        private readonly IManufacturerRepository _manufacturerRepository;

        public ManufacturerHandler(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public ICommandResult Handle(CreateManufacturerCommand command)
        {
            return Task.Run(() => HandleAsync(command)).GetAwaiter().GetResult();
        }

        public ICommandResult Handle(UpdateManufacturerCommand command)
        {
            return Task.Run(() => HandleAsync(command)).GetAwaiter().GetResult();
        }

        public async Task<ICommandResult> HandleAsync(CreateManufacturerCommand command)
        {
            command.Validate();

            if(!command.IsValid)
            {
                new CommandResult(false, command.Errors);
            }

            var manufacturer = Manufacturer.New(command.Name);
            manufacturer.Email = command.Email;
            manufacturer.Phone = command.Phone;

            await _manufacturerRepository.CreateAsync(manufacturer);

            return new CommandResult(true, "Manufacturer successfully created.");
        }

        public async Task<ICommandResult> HandleAsync(UpdateManufacturerCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                new CommandResult(false, command.Errors);
            }

            var manufacturer = new Manufacturer(command.Id, command.Name)
            {
                Email = command.Email,
                Phone = command.Phone
            };

            await _manufacturerRepository.UpdateAsync(manufacturer);

            return new CommandResult(true, "Manufacturer successfully updated.");
        }
    }
}
