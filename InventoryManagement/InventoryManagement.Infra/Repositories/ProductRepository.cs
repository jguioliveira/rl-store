using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repositories;
using InventoryManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryContext _db;

        public ProductRepository(InventoryContext db)
        {
            _db = db;
        }

        public async Task CreateAsync(Product product)
        {
            _db.Add(product);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(params string[] code)
        {
            return await _db.Products.AnyAsync(p => code.Contains(p.Code));
        }

        public async Task<Product> GetAsync(string id)
        {
            return await _db.Products.SingleOrDefaultAsync(p => p.Id == id);
        }

        public async Task UpdateAsync(Product product)
        {
            _db.Update(product);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateInventoryAsync(Inventory inventory)
        {
            _db.Update(inventory);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateProductBookMarkAsync(IEnumerable<ProductBookMark> productBookMarks)
        {
            _db.UpdateRange(productBookMarks);
            await _db.SaveChangesAsync();
        }
    }
}
