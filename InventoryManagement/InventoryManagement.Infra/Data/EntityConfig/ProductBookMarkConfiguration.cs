using InventoryManagement.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.Data.EntityConfig
{
    public class ProductBookMarkConfiguration : IEntityTypeConfiguration<ProductBookMark>
    {
        public void Configure(EntityTypeBuilder<ProductBookMark> builder)
        {
            builder.HasKey(b => new { b.ProductId, b.Code });

            builder.Property(b => b.ProductId)
                .HasMaxLength(36);

            builder.Property(e => e.Name)
               .HasMaxLength(50)
               .IsRequired()
               .IsUnicode(false);

            builder.Property(e => e.Code)
               .HasMaxLength(20)
               .IsRequired()
               .IsUnicode(false);

            builder.Property(e => e.Value)
               .HasMaxLength(30)
               .IsRequired()
               .IsUnicode(false);

            builder.HasOne(b => b.Product)
                .WithMany(b => b.ProductBookMarks)
                .HasConstraintName("FK_ProductBookMarks_Product")
                .HasForeignKey(p => p.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
