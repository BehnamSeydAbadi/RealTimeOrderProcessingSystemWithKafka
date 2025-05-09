using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.OutboxMessages;

public class OutboxMessageConfiguration : IEntityTypeConfiguration<OutboxMessageEntity>
{
    public void Configure(EntityTypeBuilder<OutboxMessageEntity> builder)
    {
        builder.HasKey(om => om.Id);
        builder.Property(om => om.Payload).HasMaxLength(int.MaxValue).IsRequired();
        builder.Property(om => om.OccurredOn).IsRequired();
        builder.Property(om => om.ProcessedOn).IsRequired(false);
        builder.Property(om => om.Error).HasMaxLength(int.MaxValue).IsRequired(false);
    }
}