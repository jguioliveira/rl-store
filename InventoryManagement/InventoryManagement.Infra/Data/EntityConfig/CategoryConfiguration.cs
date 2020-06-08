using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Data.EntityConfig
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Id)
                .ValueGeneratedNever()
                .HasMaxLength(36);

            builder.Property(e => e.Name)
               .HasMaxLength(40)
               .IsRequired()
               .IsUnicode(false);

            builder.Property(e => e.FatherCategoryId)
              .IsRequired(false)
              .IsUnicode(false);
        }
    }
}
