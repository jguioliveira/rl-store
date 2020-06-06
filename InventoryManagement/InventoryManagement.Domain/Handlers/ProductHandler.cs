using InventoryManagement.Domain.Commands;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Handlers
{
    public class ProductHandler : 
        IHandler<CreateProductCommand>,
        IHandler<UpdateProductCommand>,
        IHandler<UpdateInventoryCommand>,
        IHandler<UpdateBookMarksCommand>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IManufacturerRepository _manufacturerRepository;

        public ProductHandler(
            IProductRepository productRepository,
            ICategoryRepository categoryRepository,
            IManufacturerRepository manufacturerRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        public ICommandResult Handle(CreateProductCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(UpdateProductCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(UpdateInventoryCommand command)
        {
            throw new NotImplementedException();
        }

        public ICommandResult Handle(UpdateBookMarksCommand command)
        {
            throw new NotImplementedException();
        }

        public async Task<ICommandResult> HandleAsync(CreateProductCommand command)
        {
            var validate = await Validate(command);
            if (!validate.Item1)
            {
                return new CommandResult(false, "error message");
            }

            var product = Product.New(command.Name, validate.Item2, validate.Item3);

            await _productRepository.CreateAsync(product);

            return new CommandResult(true, "Product successfully created.");
        }

        public async Task<ICommandResult> HandleAsync(UpdateProductCommand command)
        {
            var validate = await Validate(command);
            if (!validate.Item1)
            {
                return new CommandResult(false, "error message");
            }

            var product = new Product(command.Id, command.Name, validate.Item2, validate.Item3);
            await _productRepository.UpdateAsync(product);

            return new CommandResult(true, "Product successfully updated.");
        }

        public async Task<ICommandResult> HandleAsync(UpdateInventoryCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new CommandResult(false, "error message");
            }

            var product = await _productRepository.GetAsync(command.ProductId);

            if(product is null)
            {
                return new CommandResult(false, "Product not found");
            }

            product.UpdateInventory(command.Count, command.MinCount);

            await _productRepository.UpdateInventoryAsync(product.Inventory);

            return new CommandResult(true, "Inventory successfully updated.");
        }

        public async Task<ICommandResult> HandleAsync(UpdateBookMarksCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new CommandResult(false, "error message");
            }

            var product = await _productRepository.GetAsync(command.ProductId);

            if (product is null)
            {
                return new CommandResult(false, "Product not found");
            }

            if (product.ProductBookMarks is null || !product.ProductBookMarks.Any())
                product.ClearProductBookMarks();

            foreach (var item in command.BookMarks)
            {
                product.AddProductBookMarks(item.Code, item.Name, item.Value, item.Count);
            }

            await _productRepository.UpdateProductBookMarkAsync(product.ProductBookMarks);

            return new CommandResult(true, "Product Bookmarks successfully updated.");
        }

        async Task<Tuple<bool, Category, Manufacturer>> Validate(CreateProductCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return Tuple.Create<bool, Category, Manufacturer>(false, null, null);
            }

            var exists = await _productRepository.ExistsAsync(command.Code);
            if (exists)
            {
                command.AddError("Code already registered.");
                return Tuple.Create<bool, Category, Manufacturer>(false, null, null);
            }

            var taskCategory = _categoryRepository.GetAsync(command.CategoryId);
            var taskManufacturer = _manufacturerRepository.GetAsync(command.ManufacturerId);

            await Task.WhenAll(taskCategory, taskManufacturer);

            var category = taskCategory.Result;
            var manufacturer = taskManufacturer.Result;

            if (category is null)
            {
                command.AddError("Category not found.");
                return Tuple.Create<bool, Category, Manufacturer>(false, null, null);
            }

            if (manufacturer is null)
            {
                command.AddError("Manufacturer not found.");
                return Tuple.Create<bool, Category, Manufacturer>(false, null, null);
            }

            return Tuple.Create<bool, Category, Manufacturer>(true, category, manufacturer);
        }
    }
}
