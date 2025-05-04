using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.Inventory;

public class InventoryConfiguration : IEntityTypeConfiguration<InventoryEntity>
{
    public void Configure(EntityTypeBuilder<InventoryEntity> builder)
    {
        builder.HasKey(i => i.Id);
        builder.Property(i => i.Code).HasMaxLength(128).IsRequired();
        builder.Property(i => i.Name).HasMaxLength(128).IsRequired();
        builder.Property(i => i.Location).HasMaxLength(256).IsRequired();
        builder.Property(i => i.Description).HasMaxLength(256).IsRequired();
        builder.Property(i => i.IsActive).IsRequired();
        builder.Property(i => i.RegisteredAt).IsRequired();
    }
}