using InventoryManagement.Domain.Entities;
using InventoryManagement.Infrastructure.Data.EntityConfig;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Context
{
    public class InventoryContext : DbContext
    {
        public DbSet<Product> Products {get; set;}
        public DbSet<Category> Categories { get; set; }
        public DbSet<Manufacturer>  Manufacturers { get; set; }
        public DbSet<Inventory> Inventories { get; set; }
        public DbSet<ProductBookMark> ProductBookMarks { get; set; }

        public InventoryContext(DbContextOptions<InventoryContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CategoryConfiguration());
            modelBuilder.ApplyConfiguration(new ManufacturerConfiguration());
            modelBuilder.ApplyConfiguration(new ProductConfiguration());
            modelBuilder.ApplyConfiguration(new ProductBookMarkConfiguration());
            modelBuilder.ApplyConfiguration(new InventoryConfiguration());
        }
    }
}
