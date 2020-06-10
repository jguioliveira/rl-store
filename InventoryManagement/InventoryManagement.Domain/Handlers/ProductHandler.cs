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
            var isValid = await Validate(command);
            if (!isValid)
            {
                return new CommandResult(false, command.Errors);
            }

            var product = Product.New(command.Name, command.CategoryId, command.ManufacturerId);

            await _productRepository.CreateAsync(product);

            return new CommandResult(true, "Product successfully created.");
        }

        public async Task<ICommandResult> HandleAsync(UpdateProductCommand command)
        {
            var isValid = await Validate(command);
            if (!isValid)
            {
                return new CommandResult(false, command.Errors);
            }

            var product = new Product(command.Id, command.Name, command.CategoryId, command.ManufacturerId);
            await _productRepository.UpdateAsync(product);

            return new CommandResult(true, "Product successfully updated.");
        }

        public async Task<ICommandResult> HandleAsync(UpdateInventoryCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return new CommandResult(false, command.Errors);
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
                return new CommandResult(false, command.Errors);
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

        async Task<bool> Validate(CreateProductCommand command)
        {
            command.Validate();

            if (!command.IsValid)
            {
                return false;
            }

            var exists = await _productRepository.ExistsAsync(command.Code);
            if (exists)
            {
                command.AddError("Code already registered.");
                return false;
            }

            var taskCategory = _categoryRepository.GetAsync(command.CategoryId);
            var taskManufacturer = _manufacturerRepository.GetAsync(command.ManufacturerId);

            await Task.WhenAll(taskCategory, taskManufacturer);

            var category = taskCategory.Result;
            var manufacturer = taskManufacturer.Result;

            if (category is null)
            {
                command.AddError("Category not found.");
                return false;
            }

            if (manufacturer is null)
            {
                command.AddError("Manufacturer not found.");
                return false;
            }

            return true;
        }
    }
}
