using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.InventoryProduct;

public class InventoryProductConfiguration : IEntityTypeConfiguration<InventoryProductEntity>
{
    public void Configure(EntityTypeBuilder<InventoryProductEntity> builder)
    {
        builder.HasKey(ip => ip.Id);

        builder.HasOne(ip => ip.Inventory).WithMany(i => i.InventoryProducts).HasForeignKey(ip => ip.InventoryId)
            .IsRequired();

        builder.HasOne(ip => ip.Product).WithMany(p => p.InventoryProducts).HasForeignKey(ip => ip.ProductId)
            .IsRequired();

        builder.HasIndex(
            nameof(InventoryProductEntity.InventoryId), nameof(InventoryProductEntity.ProductId)
        ).IsUnique();
    }
}