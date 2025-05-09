using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryService.Infrastructure.InboxMessages;

public class InboxMessageConfiguration : IEntityTypeConfiguration<InboxMessageEntity>
{
    public void Configure(EntityTypeBuilder<InboxMessageEntity> builder)
    {
        builder.HasKey(im => im.Id);
        builder.Property(im => im.Name).HasMaxLength(256).IsRequired();
        builder.Property(im => im.Payload).HasMaxLength(int.MaxValue).IsRequired();
        builder.Property(im => im.ReceivedOn).IsRequired();
        builder.Property(im => im.ProcessedOn).IsRequired(false);
        builder.Property(im => im.Error).HasMaxLength(int.MaxValue).IsRequired(false);
    }
}