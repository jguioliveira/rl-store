using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Data.EntityConfig
{
    public class ManufacturerConfiguration : IEntityTypeConfiguration<Manufacturer>
    {
        public void Configure(EntityTypeBuilder<Manufacturer> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ValueGeneratedNever()
                .HasMaxLength(36);

            builder.Property(e => e.Name)
               .HasMaxLength(50)
               .IsRequired()
               .IsUnicode(false);

            builder.Property(e => e.Email)
               .HasMaxLength(50)
               .IsRequired(false)
               .IsUnicode(false);

            builder.Property(e => e.Phone)
               .HasMaxLength(20)
               .IsRequired(false)
               .IsUnicode(false);
        }
    }
}
