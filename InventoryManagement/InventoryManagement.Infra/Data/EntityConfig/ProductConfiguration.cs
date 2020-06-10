using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Data.EntityConfig
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ValueGeneratedNever()
                .HasMaxLength(36);

            builder.Property(e => e.Name)
               .HasMaxLength(50)
               .IsRequired()
               .IsUnicode(false);

            builder.Property(e => e.Code)
               .HasMaxLength(20)
               .IsRequired()
               .IsUnicode(false);

            builder.HasOne(p => p.Inventory)
                .WithOne(i => i.Product)
                .HasForeignKey<Inventory>(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.CategoryId)
                .IsRequired();
            builder.HasOne(b => b.Category)
                .WithMany(b => b.Products)
                .HasConstraintName("FK_Product_Category")
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(p => p.ManufacturerId)
                .IsRequired();
            builder.HasOne(b => b.Manufacturer)
                .WithMany(b => b.Products)
                .HasConstraintName("FK_Product_Manufacturer")
                .HasForeignKey(p => p.ManufacturerId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
