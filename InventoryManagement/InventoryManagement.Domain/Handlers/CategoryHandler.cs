using InventoryManagement.Domain.Commands;
using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Handlers
{
    public class CategoryHandler : 
        IHandler<CreateCategoryCommand>,
        IHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public ICommandResult Handle(CreateCategoryCommand command)
        {
            return Task.Run(() => HandleAsync(command)).GetAwaiter().GetResult();
        }

        public ICommandResult Handle(UpdateCategoryCommand command)
        {
            return Task.Run(() => HandleAsync(command)).GetAwaiter().GetResult();
        }

        public async Task<ICommandResult> HandleAsync(CreateCategoryCommand command)
        {
            //validar objeto recebido
            command.Validate();

            if (!command.IsValid)
            {
                new CommandResult(false, command.Errors);
            }

            //validar se já existe categoria com nome enviado
            bool exists = await _categoryRepository.ContainsAsync(command.Name);
            if (exists)
            {
                new CommandResult(false, "Already exists a category with this name.");
            }

            //cadastrar categoria
            List<Category> categories = new List<Category>();
            var category = Category.New(command.Name);
            categories.Add(category);

            if(!(command.Children is null) && command.Children.Any())
            {
                var children = GetRecursiveCategories(command.Children, category.Id, level: 0);
                categories.AddRange(children);
            }            

            await _categoryRepository.CreateAsync(categories);

            return new CommandResult(true, "Category successfully created.");
        }

        public async Task<ICommandResult> HandleAsync(UpdateCategoryCommand command)
        {
            //validar objeto recebido
            command.Validate();

            if (!command.IsValid)
            {
                new CommandResult(false, command.Errors);
            }

            //validar se já existe categoria com nome enviado
            bool exists = await _categoryRepository.ContainsAtAsync(command.FatherCategoryId, command.Name);
            if (exists)
            {
                new CommandResult(false, "Already exists a category with this name.");
            }

            //validar se pai existe
            if (!string.IsNullOrEmpty(command.FatherCategoryId))
            {
                exists = await _categoryRepository.ExistsAsync(command.FatherCategoryId);
                if (!exists)
                {
                    new CommandResult(false, "Invalid father category.");
                }
            }            

            //atualizar categoria
            var category = new Category(command.Id, command.Name, command.FatherCategoryId);

            await _categoryRepository.UpdateAsync(category);

            return new CommandResult(true, "Category successfully updated.");
        }

        private IEnumerable<Category> GetRecursiveCategories(IEnumerable<CreateCategoryCommand.CategoryData> categories, string fatherCategoryId, int level)
        {
            List<Category> result = new List<Category>();
            foreach (var item in categories)
            {                
                var category = Category.New(item.Name, fatherCategoryId);
                result.Add(category);

                if (level < Category.MAX_SUBCATEGORIES)
                    if(!(item.Children is null) && item.Children.Any())
                    {
                        var childrenValues = GetRecursiveCategories(item.Children, category.Id, level+1);
                        result.AddRange(childrenValues);
                    }
            }

            return result;
        }
    }
}
