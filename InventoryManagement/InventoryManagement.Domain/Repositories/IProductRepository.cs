using InventoryManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Repositories
{
    public interface IProductRepository
    {
        Task<bool> ExistsAsync(params string[] code);
        Task CreateAsync(Product product);
        Task UpdateAsync(Product product);
        Task UpdateInventoryAsync(Inventory inventory);
        Task UpdateProductBookMarkAsync(IEnumerable<ProductBookMark> productBookMarks);
        Task<Product> GetAsync(string id);
    }
}
