using InventoryManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Domain.Repositories
{
    public interface IManufacturerRepository
    {
        Task CreateAsync(Manufacturer manufacturer);
        Task UpdateAsync(string id, Manufacturer manufacturer);
        Task DeleteAsync(string id);
        Task<Manufacturer> GetAsync(string id);
        Task<IEnumerable<Manufacturer>> GetAsync();
    }
}
