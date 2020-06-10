using InventoryManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Repositories
{
    public interface ICategoryRepository
    {
        Task CreateAsync(IEnumerable<Category> categories);
        Task UpdateAsync(Category category);
        Task<bool> ContainsAsync(string name);
        Task<bool> ContainsAtAsync(string fatherCategoryId, string name);
        Task<bool> ExistsAsync(string id);
        Task<Category> GetAsync(string id);
        Task<IEnumerable<Category>> GetAsync();
    }
}
