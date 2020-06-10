using InventoryManagement.Domain.Entities;
using InventoryManagement.Domain.Repositories;
using InventoryManagement.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly InventoryContext _db;

        public CategoryRepository(InventoryContext db)
        {
            _db = db;
        }

        public async Task<bool> ContainsAsync(string name)
        {
            return await _db.Categories.AnyAsync(c => c.Name == name);
        }

        public async Task<bool> ContainsAtAsync(string fatherCategoryId, string name)
        {
            return await _db.Categories.AnyAsync(c => c.Name == name && c.FatherCategoryId == fatherCategoryId);
        }

        public async Task CreateAsync(IEnumerable<Category> categories)
        {
            _db.AddRange(categories);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> ExistsAsync(string id)
        {
            return await _db.Categories.AnyAsync(c => c.Id == id);
        }

        public async Task<Category> GetAsync(string id)
        {
            return await _db.Categories
                .SingleOrDefaultAsync(c => c.Id == id);
        }

        public async Task<IEnumerable<Category>> GetAsync()
        {
            return await _db.Categories
                .ToListAsync();
        }

        public async Task UpdateAsync(Category category)
        {
            _db.Update(category);
            await _db.SaveChangesAsync();
        }
    }
}
