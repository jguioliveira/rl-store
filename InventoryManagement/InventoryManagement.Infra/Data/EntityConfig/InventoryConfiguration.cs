using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Data.EntityConfig
{
    public class InventoryConfiguration : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.HasKey(i => i.ProductId);
            builder.Property(i => i.ProductId)
                .ValueGeneratedNever()
                .HasMaxLength(36); 

            builder.Property(e => e.CreatedOn).HasColumnType("datetime");

            builder.Property(e => e.LastChange).HasColumnType("datetime");
        }
    }
}
