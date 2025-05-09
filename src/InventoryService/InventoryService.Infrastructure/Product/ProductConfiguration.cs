using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Product;

public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>
{
    public void Configure(EntityTypeBuilder<ProductEntity> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Name).HasMaxLength(128).IsRequired();
        builder.Property(p => p.StockKeepingUnit).HasMaxLength(32).IsRequired();
        builder.Property(p => p.Color).HasMaxLength(16).IsRequired();
        builder.Property(p => p.UnitOfMeasure).HasConversion<int>().IsRequired();
        builder.Property(p => p.IsPerishable).IsRequired();
        builder.Property(p => p.Dimensions).HasMaxLength(128).IsRequired();
        builder.Property(p => p.Weight).HasMaxLength(16).IsRequired();
        builder.Property(p => p.Category).HasConversion<int>().IsRequired();

        builder.Seed();
    }
}