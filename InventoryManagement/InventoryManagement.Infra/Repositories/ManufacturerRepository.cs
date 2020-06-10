using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repositories;
using InventoryManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private readonly InventoryContext _db;

        public ManufacturerRepository(InventoryContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Manufacturer manufacturer)
        {
            _db.Add(manufacturer);
            await _db.SaveChangesAsync();
        }

        public async Task<Manufacturer> GetAsync(string id)
        {
            return await _db.Manufacturers.SingleOrDefaultAsync(m => m.Id == id);
        }

        public async Task<IEnumerable<Manufacturer>> GetAsync()
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(Manufacturer manufacturer)
        {
            _db.Update(manufacturer);
            await _db.SaveChangesAsync();
        }
    }
}
